using LibrarySystemPatterns.Patterns.Composite;
using LibrarySystemPatterns;

namespace LibrarySystemPatterns.Patterns.Proxy;

// прокси - контролирует доступ к операциям библиотеки
public class LibraryManagerProxy : ILibraryManagement
{
    private readonly RealLibraryManagement _realLibraryManagement;
    private readonly UserRole _userRole;

    public LibraryManagerProxy(UserRole userRole, BookCategory catalog)
    {
        _userRole = userRole;
        _realLibraryManagement = new RealLibraryManagement(catalog ?? throw new ArgumentNullException(nameof(catalog)));
    }

    // только админ может добавлять книги
    public void AddBookToCatalog(string title)
    {
        if (_userRole == UserRole.Admin)
        {
            _realLibraryManagement.AddBookToCatalog(title);
        }
        else
        {
            Console.WriteLine("Access Denied: Only administrators can add books");
        }
    }

    // пользователи и админы могут оформлять книги
    public void CheckoutBook(string title, UserRole userRole)
    {
        if (_userRole == UserRole.User || _userRole == UserRole.Admin)
        {
            _realLibraryManagement.CheckoutBook(title, userRole);
        }
        else
        {
            Console.WriteLine($"Access Denied: Role '{_userRole}' cannot checkout books");
        }
    }

    // пользователи и админы могут возвращать книги
    public void ReturnBook(string title)
    {
        if (_userRole == UserRole.User || _userRole == UserRole.Admin)
        {
            _realLibraryManagement.ReturnBook(title);
        }
        else
        {
            Console.WriteLine($"Access Denied: Role '{_userRole}' cannot return books");
        }
    }
}
