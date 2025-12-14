namespace LibrarySystemPatterns.Patterns.Flyweight;

/// <summary>
/// Factory class that manages Author objects using the Flyweight pattern.
/// Ensures that only one instance of an Author with a given name exists,
/// reusing existing instances to optimize memory usage.
/// </summary>
public class AuthorFactory
{
    private readonly Dictionary<string, Author> _authors = new();

    /// <summary>
    /// Gets an Author with the specified name.
    /// If an Author with this name already exists, returns the existing instance.
    /// Otherwise, creates a new Author, stores it, and returns it.
    /// </summary>
    /// <param name="name">The name of the author</param>
    /// <returns>An Author instance with the specified name</returns>
    public Author GetAuthor(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Author name cannot be null or empty.", nameof(name));

        if (!_authors.ContainsKey(name))
        {
            _authors[name] = new Author(name);
        }

        return _authors[name];
    }

    /// <summary>
    /// Gets the total number of unique authors stored in the factory.
    /// </summary>
    public int AuthorCount => _authors.Count;
}


