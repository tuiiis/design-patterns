using LibrarySystemPatterns.Patterns.Composite;

namespace LibrarySystemPatterns.Patterns.Proxy;

// прокси - контролирует доступ к операциям библиотеки
public class LibraryManagerProxy : ILibraryManagement
{
    private readonly RealLibraryManagement _realLibraryManagement;
    private readonly string _userRole;

    public LibraryManagerProxy(string userRole, BookCategory catalog)
    {
        _userRole = userRole ?? throw new ArgumentNullException(nameof(userRole));
        _realLibraryManagement = new RealLibraryManagement(catalog ?? throw new ArgumentNullException(nameof(catalog)));
    }

    // только админ может добавлять книги
    public void AddBookToCatalog(string title)
    {
        if (_userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
        {
            _realLibraryManagement.AddBookToCatalog(title);
        }
        else
        {
            Console.WriteLine("доступ запрещен: только администраторы могут добавлять книги");
        }
    }

    // пользователи и админы могут оформлять книги
    public void CheckoutBook(string title, string userRole)
    {
        if (_userRole.Equals("User", StringComparison.OrdinalIgnoreCase) || 
            _userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
        {
            _realLibraryManagement.CheckoutBook(title, userRole);
        }
        else
        {
            Console.WriteLine($"доступ запрещен: роль '{_userRole}' не может оформлять книги");
        }
    }

    // пользователи и админы могут возвращать книги
    public void ReturnBook(string title)
    {
        if (_userRole.Equals("User", StringComparison.OrdinalIgnoreCase) || 
            _userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
        {
            _realLibraryManagement.ReturnBook(title);
        }
        else
        {
            Console.WriteLine($"доступ запрещен: роль '{_userRole}' не может возвращать книги");
        }
    }
}
