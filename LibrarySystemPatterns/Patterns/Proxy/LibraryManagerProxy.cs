using LibrarySystemPatterns.Patterns.Composite;
using LibrarySystemPatterns;

namespace LibrarySystemPatterns.Patterns.Proxy;

// прокси - контролирует доступ к операциям библиотеки
public class LibraryManagerProxy : ILibraryManagement
{
    private readonly RealLibraryManagement _realLibraryManagement;
    private readonly User _user;

    public LibraryManagerProxy(User user, BookCategory catalog)
    {
        _user = user ?? throw new ArgumentNullException(nameof(user));
        _realLibraryManagement = new RealLibraryManagement(catalog ?? throw new ArgumentNullException(nameof(catalog)));
    }

    // только админ может добавлять книги
    public void AddBookToCatalog(string title)
    {
        if (_user.Role == UserRole.Admin)
        {
            _realLibraryManagement.AddBookToCatalog(title);
        }
        else
        {
            Console.WriteLine("Access Denied: Only administrators can add books");
        }
    }

    // пользователи и админы могут оформлять книги
    public void CheckoutBook(string title, string userName)
    {
        if (_user.Role == UserRole.User || _user.Role == UserRole.Admin)
        {
            // Security: Regular users can only checkout as themselves
            if (_user.Role == UserRole.User && userName != _user.Name)
            {
                Console.WriteLine($"Access Denied: Users can only checkout books as themselves");
                return;
            }
            
            _realLibraryManagement.CheckoutBook(title, userName);
        }
        else
        {
            Console.WriteLine($"Access Denied: Role '{_user.Role}' cannot checkout books");
        }
    }

    // пользователи и админы могут возвращать книги
    public void ReturnBook(string title)
    {
        if (_user.Role == UserRole.User || _user.Role == UserRole.Admin)
        {
            _realLibraryManagement.ReturnBook(title);
        }
        else
        {
            Console.WriteLine($"Access Denied: Role '{_user.Role}' cannot return books");
        }
    }
}
