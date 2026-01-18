using LibrarySystemPatterns.Patterns.Composite;
using LibrarySystemPatterns.Patterns.Decorator;
using LibrarySystemPatterns;

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
            Console.WriteLine("Error: Book title cannot be empty");
            return;
        }

        Console.WriteLine($"Book '{title}' added to catalog");
    }

    // оформляем книгу
    public void CheckoutBook(string title, UserRole userRole)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Error: Book title cannot be empty");
            return;
        }

        var book = FindBookInCatalog(title, _catalog);
        if (book == null)
        {
            Console.WriteLine($"Error: Book '{title}' not found in catalog");
            return;
        }

        if (book.IsCheckedOut)
        {
            Console.WriteLine($"Error: Book '{title}' is already checked out to {book.CheckedOutBy}");
            return;
        }

        book.IsCheckedOut = true;
        book.CheckedOutBy = userRole.ToString();
        Console.WriteLine($"Book '{title}' checked out to {userRole}");
    }

    // возвращаем книгу
    public void ReturnBook(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Error: Book title cannot be empty");
            return;
        }

        var book = FindBookInCatalog(title, _catalog);
        if (book == null)
        {
            Console.WriteLine($"Error: Book '{title}' not found in catalog");
            return;
        }

        if (!book.IsCheckedOut)
        {
            Console.WriteLine($"Error: Book '{title}' is not checked out");
            return;
        }

        string previousBorrower = book.CheckedOutBy ?? "Unknown";
        book.IsCheckedOut = false;
        book.CheckedOutBy = null;
        Console.WriteLine($"Book '{title}' returned (was checked out to: {previousBorrower})");
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
