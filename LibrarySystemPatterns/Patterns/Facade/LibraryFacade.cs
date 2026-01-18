using LibrarySystemPatterns.Patterns.Flyweight;
using LibrarySystemPatterns.Patterns.Composite;
using LibrarySystemPatterns.Patterns.Decorator;
using LibrarySystemPatterns.Patterns.Adapter;
using LibrarySystemPatterns.Patterns.Proxy;

namespace LibrarySystemPatterns.Patterns.Facade;

// фасад - упрощенный интерфейс для работы с библиотекой
public class LibraryFacade
{
    private readonly AuthorFactory _authorFactory;
    private readonly BookCategory _rootCategory;
    private readonly ISearchSystem _searchSystem;
    private readonly ILibraryManagement _libraryManagement;
    private readonly string _userRole;

    public LibraryFacade(string userRole)
    {
        if (string.IsNullOrWhiteSpace(userRole))
            throw new ArgumentException("роль пользователя не может быть пустой", nameof(userRole));

        _userRole = userRole;

        // инициализируем все подсистемы
        _authorFactory = new AuthorFactory();
        _rootCategory = new BookCategory("каталог библиотеки");
        
        var oldSearchSystem = new OldSearchSystem();
        _searchSystem = new SearchSystemAdapter(oldSearchSystem);
        
        _libraryManagement = new LibraryManagerProxy(userRole, _rootCategory);
    }

    // добавляем книгу в категорию (только для админа)
    public void AddBook(string category, string title, string author)
    {
        // проверяем права через прокси
        _libraryManagement.AddBookToCatalog(title);

        // только админ может добавлять
        if (!_userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        // получаем автора через flyweight
        var authorObj = _authorFactory.GetAuthor(author);
        var book = new Book(title, authorObj);
        var targetCategory = FindOrCreateCategory(category);
        targetCategory.Add(book);
    }

    // поиск через адаптер
    public void Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            Console.WriteLine("ошибка: запрос не может быть пустым");
            return;
        }

        _searchSystem.Search(query);
    }

    // показываем весь каталог
    public void ShowCatalog()
    {
        Console.WriteLine("каталог библиотеки:");
        Console.WriteLine("==================");
        _rootCategory.Display(0);
    }

    // добавляем рейтинг к книге через декоратор
    public void RateBook(string title, double rating)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("ошибка: название книги не может быть пустым");
            return;
        }

        var (bookComponent, parentCategory) = FindBookInCatalog(title, _rootCategory);

        if (bookComponent == null || parentCategory == null)
        {
            Console.WriteLine($"ошибка: книга '{title}' не найдена в каталоге");
            return;
        }

        // если книга уже декорирована, берем оригинал
        LibraryComponent originalBook = bookComponent;
        if (bookComponent is RatingDecorator decorator)
        {
            originalBook = decorator.Component;
        }

        // создаем новую декорированную версию
        var ratedBook = new RatingDecorator(originalBook, rating);

        // заменяем в каталоге
        if (parentCategory.Remove(bookComponent))
        {
            parentCategory.Add(ratedBook);
            Console.WriteLine($"книге '{title}' присвоен рейтинг {rating:F1}");
        }
        else
        {
            Console.WriteLine($"ошибка: не удалось обновить рейтинг для книги '{title}'");
        }
    }

    // оформляем книгу через прокси
    public void CheckoutBook(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("ошибка: название книги не может быть пустым");
            return;
        }

        _libraryManagement.CheckoutBook(title, _userRole);
    }

    // возвращаем книгу через прокси
    public void ReturnBook(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("ошибка: название книги не может быть пустым");
            return;
        }

        _libraryManagement.ReturnBook(title);
    }

    // находим или создаем категорию
    private BookCategory FindOrCreateCategory(string categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            return _rootCategory;
        }

        var category = FindCategoryInCatalog(categoryName, _rootCategory);
        
        if (category != null)
        {
            return category;
        }

        var newCategory = new BookCategory(categoryName);
        _rootCategory.Add(newCategory);
        return newCategory;
    }

    // ищем категорию в каталоге рекурсивно
    private BookCategory? FindCategoryInCatalog(string categoryName, BookCategory currentCategory)
    {
        if (currentCategory.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase))
        {
            return currentCategory;
        }

        foreach (var child in currentCategory.GetChildren())
        {
            if (child is BookCategory childCategory)
            {
                var found = FindCategoryInCatalog(categoryName, childCategory);
                if (found != null)
                {
                    return found;
                }
            }
        }

        return null;
    }

    // ищем книгу в каталоге рекурсивно
    private (LibraryComponent? book, BookCategory? parentCategory) FindBookInCatalog(string title, BookCategory category)
    {
        foreach (var child in category.GetChildren())
        {
            if (child is Book book && book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                return (book, category);
            }

            // проверяем декорированную книгу
            if (child is RatingDecorator decorator && 
                decorator.Component is Book decoratedBook &&
                decoratedBook.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                return (child, category);
            }

            // ищем в подкатегориях
            if (child is BookCategory childCategory)
            {
                var result = FindBookInCatalog(title, childCategory);
                if (result.book != null)
                {
                    return result;
                }
            }
        }

        return (null, null);
    }
}
