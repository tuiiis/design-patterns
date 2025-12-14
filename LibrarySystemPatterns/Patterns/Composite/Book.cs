using LibrarySystemPatterns.Patterns.Flyweight;

namespace LibrarySystemPatterns.Patterns.Composite;

/// <summary>
/// Represents a Book (Leaf) in the Composite Pattern.
/// A Book has a Title and an Author reference (using Flyweight pattern).
/// </summary>
public class Book : LibraryComponent
{
    public Author Author { get; }
    public string Title => Name; // Title is stored in the Name property

    public Book(string title, Author author) : base(title)
    {
        Author = author ?? throw new ArgumentNullException(nameof(author));
    }

    /// <summary>
    /// Displays the book with indentation based on depth.
    /// </summary>
    /// <param name="depth">The indentation depth</param>
    public override void Display(int depth)
    {
        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}- Book: {Title} by {Author.Name}");
    }
}


