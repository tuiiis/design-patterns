namespace LibrarySystemPatterns.Patterns.Proxy;

/// <summary>
/// Proxy class that controls access to library management operations.
/// Implements access control based on user roles.
/// </summary>
public class LibraryManagerProxy : ILibraryManagement
{
    private readonly RealLibraryManagement _realLibraryManagement;
    private readonly string _userRole;

    /// <summary>
    /// Creates a new proxy instance with the specified user role.
    /// </summary>
    /// <param name="userRole">The role of the user attempting to access library management</param>
    public LibraryManagerProxy(string userRole)
    {
        _userRole = userRole ?? throw new ArgumentNullException(nameof(userRole));
        _realLibraryManagement = new RealLibraryManagement();
    }

    /// <summary>
    /// Adds a book to the catalog if the user has Admin role.
    /// Otherwise, access is denied.
    /// </summary>
    /// <param name="title">The title of the book to add</param>
    public void AddBookToCatalog(string title)
    {
        if (_userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
        {
            _realLibraryManagement.AddBookToCatalog(title);
        }
        else
        {
            Console.WriteLine("Access Denied: Only Administrators can add books to the catalog.");
        }
    }
}


