using LibrarySystemPatterns.Patterns.Flyweight;
using LibrarySystemPatterns.Patterns.Composite;
using LibrarySystemPatterns.Patterns.Decorator;
using LibrarySystemPatterns.Patterns.Adapter;
using LibrarySystemPatterns.Patterns.Proxy;

namespace LibrarySystemPatterns.Patterns.Facade;

/// <summary>
/// Facade class that provides a simplified interface to the library system.
/// Hides the complexity of interacting with multiple subsystems and patterns.
/// </summary>
public class LibraryFacade
{
    private readonly AuthorFactory _authorFactory;
    private readonly BookCategory _rootCategory;
    private readonly ISearchSystem _searchSystem;
    private readonly ILibraryManagement _libraryManagement;
    private readonly string _userRole;

    /// <summary>
    /// Creates a new LibraryFacade with the specified user role.
    /// </summary>
    /// <param name="userRole">The role of the current user (e.g., "Admin", "User")</param>
    public LibraryFacade(string userRole)
    {
        if (string.IsNullOrWhiteSpace(userRole))
            throw new ArgumentException("User role cannot be null or empty.", nameof(userRole));

        _userRole = userRole;

        // Initialize all subsystems
        _authorFactory = new AuthorFactory();
        _rootCategory = new BookCategory("Library Catalog");
        
        var oldSearchSystem = new OldSearchSystem();
        _searchSystem = new SearchSystemAdapter(oldSearchSystem);
        
        _libraryManagement = new LibraryManagerProxy(userRole);
    }

    /// <summary>
    /// Adds a book to the specified category.
    /// Uses the Proxy to check permissions before adding.
    /// </summary>
    /// <param name="category">The category name where the book should be added</param>
    /// <param name="title">The title of the book</param>
    /// <param name="author">The name of the author</param>
    public void AddBook(string category, string title, string author)
    {
        // Check permissions using the proxy
        _libraryManagement.AddBookToCatalog(title);

        // Only proceed if user has Admin role
        if (!_userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
        {
            return; // Access denied, don't add the book
        }

        // Get or create the author using Flyweight pattern
        var authorObj = _authorFactory.GetAuthor(author);

        // Create the book
        var book = new Book(title, authorObj);

        // Find or create the category
        var targetCategory = FindOrCreateCategory(category);

        // Add book to category
        targetCategory.Add(book);
    }

    /// <summary>
    /// Performs a search using the search system.
    /// </summary>
    /// <param name="query">The search query</param>
    public void Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            Console.WriteLine("Error: Search query cannot be empty.");
            return;
        }

        _searchSystem.Search(query);
    }

    /// <summary>
    /// Displays the entire catalog structure.
    /// </summary>
    public void ShowCatalog()
    {
        Console.WriteLine("Library Catalog:");
        Console.WriteLine("================");
        _rootCategory.Display(0);
    }

    /// <summary>
    /// Adds a rating to a book by decorating it with a RatingDecorator.
    /// </summary>
    /// <param name="title">The title of the book to rate</param>
    /// <param name="rating">The rating value (0-5)</param>
    public void RateBook(string title, double rating)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Error: Book title cannot be empty.");
            return;
        }

        // Find the book in the catalog
        var (bookComponent, parentCategory) = FindBookInCatalog(title, _rootCategory);

        if (bookComponent == null || parentCategory == null)
        {
            Console.WriteLine($"Error: Book '{title}' not found in catalog.");
            return;
        }

        // If the book is already decorated, we need to get the original book
        LibraryComponent originalBook = bookComponent;
        if (bookComponent is RatingDecorator decorator)
        {
            // Get the wrapped component
            originalBook = decorator.Component;
        }

        // Create a new decorated version with the rating
        var ratedBook = new RatingDecorator(originalBook, rating);

        // Replace the book in the catalog
        if (parentCategory.Remove(bookComponent))
        {
            parentCategory.Add(ratedBook);
            Console.WriteLine($"Book '{title}' has been rated {rating:F1} stars.");
        }
        else
        {
            Console.WriteLine($"Error: Could not update rating for book '{title}'.");
        }
    }

    /// <summary>
    /// Finds or creates a category with the specified name.
    /// </summary>
    private BookCategory FindOrCreateCategory(string categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            return _rootCategory;
        }

        // Search for existing category
        var category = FindCategoryInCatalog(categoryName, _rootCategory);
        
        if (category != null)
        {
            return category;
        }

        // Create new category if not found
        var newCategory = new BookCategory(categoryName);
        _rootCategory.Add(newCategory);
        return newCategory;
    }

    /// <summary>
    /// Recursively finds a category by name in the catalog structure.
    /// </summary>
    private BookCategory? FindCategoryInCatalog(string categoryName, BookCategory currentCategory)
    {
        if (currentCategory.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase))
        {
            return currentCategory;
        }

        // Recursively search in child categories
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

    /// <summary>
    /// Finds a book by title in the catalog structure.
    /// </summary>
    private (LibraryComponent? book, BookCategory? parentCategory) FindBookInCatalog(string title, BookCategory category)
    {
        // Search in direct children
        foreach (var child in category.GetChildren())
        {
            if (child is Book book && book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                return (book, category);
            }

            // Also check if it's a decorated book
            if (child is RatingDecorator decorator && 
                decorator.Component is Book decoratedBook &&
                decoratedBook.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                return (child, category);
            }

            // Recursively search in child categories
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

    /// <summary>
    /// Replaces a book component with a new one in the catalog.
    /// </summary>
    private void ReplaceBookInCatalog(LibraryComponent oldBook, LibraryComponent newBook, BookCategory category)
    {
        if (category.Remove(oldBook))
        {
            category.Add(newBook);
        }
        else
        {
            // Recursively search in child categories
            foreach (var child in category.GetChildren())
            {
                if (child is BookCategory childCategory)
                {
                    ReplaceBookInCatalog(oldBook, newBook, childCategory);
                }
            }
        }
    }
}


