namespace LibrarySystemPatterns.Patterns.Proxy;

/// <summary>
/// Real implementation of library management operations.
/// This represents the actual service that performs library management tasks.
/// </summary>
public class RealLibraryManagement : ILibraryManagement
{
    /// <summary>
    /// Adds a book to the catalog.
    /// </summary>
    /// <param name="title">The title of the book to add</param>
    public void AddBookToCatalog(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Error: Book title cannot be empty.");
            return;
        }

        Console.WriteLine($"Book '{title}' has been added to the catalog.");
    }
}


