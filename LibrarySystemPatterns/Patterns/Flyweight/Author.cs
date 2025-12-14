namespace LibrarySystemPatterns.Patterns.Flyweight;

/// <summary>
/// Represents an Author in the Flyweight pattern.
/// This class stores intrinsic (shared) state that can be reused across multiple contexts.
/// </summary>
public class Author
{
    public string Name { get; }

    public Author(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}


