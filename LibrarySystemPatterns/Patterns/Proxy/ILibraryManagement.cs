namespace LibrarySystemPatterns.Patterns.Proxy;

/// <summary>
/// Interface for library management operations.
/// </summary>
public interface ILibraryManagement
{
    /// <summary>
    /// Adds a book to the catalog.
    /// </summary>
    /// <param name="title">The title of the book to add</param>
    void AddBookToCatalog(string title);
}


