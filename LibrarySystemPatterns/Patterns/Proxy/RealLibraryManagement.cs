using LibrarySystemPatterns.Patterns.Composite;
using LibrarySystemPatterns.Patterns.Decorator;

namespace LibrarySystemPatterns.Patterns.Proxy;

// реальная реализация управления библиотекой
public class RealLibraryManagement : ILibraryManagement
{
    private readonly BookCategory _catalog;

    public RealLibraryManagement(BookCategory catalog)
    {
        _catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
    }

    // добавляем книгу в каталог
    public void AddBookToCatalog(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("ошибка: название книги не может быть пустым");
            return;
        }

        Console.WriteLine($"книга '{title}' добавлена в каталог");
    }

    // оформляем книгу
    public void CheckoutBook(string title, string userRole)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("ошибка: название книги не может быть пустым");
            return;
        }

        var book = FindBookInCatalog(title, _catalog);
        if (book == null)
        {
            Console.WriteLine($"ошибка: книга '{title}' не найдена в каталоге");
            return;
        }

        if (book.IsCheckedOut)
        {
            Console.WriteLine($"ошибка: книга '{title}' уже выдана пользователю {book.CheckedOutBy}");
            return;
        }

        book.IsCheckedOut = true;
        book.CheckedOutBy = userRole;
        Console.WriteLine($"книга '{title}' выдана пользователю {userRole}");
    }

    // возвращаем книгу
    public void ReturnBook(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("ошибка: название книги не может быть пустым");
            return;
        }

        var book = FindBookInCatalog(title, _catalog);
        if (book == null)
        {
            Console.WriteLine($"ошибка: книга '{title}' не найдена в каталоге");
            return;
        }

        if (!book.IsCheckedOut)
        {
            Console.WriteLine($"ошибка: книга '{title}' не выдана");
            return;
        }

        string previousBorrower = book.CheckedOutBy ?? "неизвестно";
        book.IsCheckedOut = false;
        book.CheckedOutBy = null;
        Console.WriteLine($"книга '{title}' возвращена (была выдана: {previousBorrower})");
    }

    // ищем книгу в каталоге рекурсивно
    private Book? FindBookInCatalog(string title, BookCategory category)
    {
        foreach (var child in category.GetChildren())
        {
            // проверяем обычную книгу
            if (child is Book book && book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                return book;
            }

            // проверяем декорированную книгу
            if (child is RatingDecorator decorator && 
                decorator.Component is Book decoratedBook &&
                decoratedBook.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                return decoratedBook;
            }

            // ищем в подкатегориях
            if (child is BookCategory childCategory)
            {
                var found = FindBookInCatalog(title, childCategory);
                if (found != null)
                {
                    return found;
                }
            }
        }

        return null;
    }
}
