namespace LibrarySystemPatterns.Patterns.Flyweight;

// автор - легковесный объект, который переиспользуется
public class Author
{
    public string Name { get; }

    public Author(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}
