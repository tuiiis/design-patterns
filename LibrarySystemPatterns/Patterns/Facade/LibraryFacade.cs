using LibrarySystemPatterns.Patterns.Flyweight;
using LibrarySystemPatterns.Patterns.Composite;
using LibrarySystemPatterns.Patterns.Decorator;
using LibrarySystemPatterns.Patterns.Adapter;
using LibrarySystemPatterns.Patterns.Proxy;
using LibrarySystemPatterns;

namespace LibrarySystemPatterns.Patterns.Facade;

// фасад - упрощенный интерфейс для работы с библиотекой
public class LibraryFacade
{
    private readonly AuthorFactory _authorFactory;
    private readonly BookCategory _rootCategory;
    private readonly ISearchSystem _searchSystem;
    private readonly ILibraryManagement _libraryManagement;
    private readonly User _user;

    public LibraryFacade(User user)
    {
        _user = user ?? throw new ArgumentNullException(nameof(user));

        // инициализируем все подсистемы
        _authorFactory = new AuthorFactory();
        _rootCategory = SharedLibraryCatalog.Instance.Catalog;
        
        var oldSearchSystem = new OldSearchSystem();
        _searchSystem = new SearchSystemAdapter(oldSearchSystem);
        
        _libraryManagement = new LibraryManagerProxy(user, _rootCategory);
    }

    // добавляем книгу в категорию (только для админа)
    public void AddBook(string category, string title, string author)
    {
        // проверяем права через прокси
        _libraryManagement.AddBookToCatalog(title);

        // только админ может добавлять
        if (_user.Role != UserRole.Admin)
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
            Console.WriteLine("Error: Query cannot be empty");
            return;
        }

        _searchSystem.Search(query);
    }

    // показываем весь каталог
    public void ShowCatalog()
    {
        Console.WriteLine("Library Catalog:");
        Console.WriteLine("===============");
        _rootCategory.Display(0);
    }

    // добавляем рейтинг к книге через декоратор
    public void RateBook(string title, double rating)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Error: Book title cannot be empty");
            return;
        }

        var (bookComponent, parentCategory) = FindBookInCatalog(title, _rootCategory);

        if (bookComponent == null || parentCategory == null)
        {
            Console.WriteLine($"Error: Book '{title}' not found in catalog");
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
            Console.WriteLine($"Rating {rating:F1} assigned to book '{title}'");
        }
        else
        {
            Console.WriteLine($"Error: Failed to update rating for book '{title}'");
        }
    }

    // оформляем книгу через прокси
    public void CheckoutBook(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Error: Book title cannot be empty");
            return;
        }

        _libraryManagement.CheckoutBook(title, _user.Name);
    }

    // возвращаем книгу через прокси
    public void ReturnBook(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Error: Book title cannot be empty");
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
