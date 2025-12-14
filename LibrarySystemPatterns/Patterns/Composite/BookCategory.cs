namespace LibrarySystemPatterns.Patterns.Composite;

/// <summary>
/// Represents a BookCategory (Composite) in the Composite Pattern.
/// A category can contain both books and other categories, forming a tree structure.
/// </summary>
public class BookCategory : LibraryComponent
{
    private readonly List<LibraryComponent> _children = new();

    public BookCategory(string name) : base(name)
    {
    }

    /// <summary>
    /// Adds a component (book or category) to this category.
    /// </summary>
    /// <param name="component">The component to add</param>
    public void Add(LibraryComponent component)
    {
        if (component == null)
            throw new ArgumentNullException(nameof(component));

        _children.Add(component);
    }

    /// <summary>
    /// Removes a component from this category.
    /// </summary>
    /// <param name="component">The component to remove</param>
    public bool Remove(LibraryComponent component)
    {
        if (component == null)
            return false;

        return _children.Remove(component);
    }

    /// <summary>
    /// Gets all children components in this category.
    /// </summary>
    /// <returns>An enumerable collection of child components</returns>
    public IEnumerable<LibraryComponent> GetChildren()
    {
        return _children;
    }

    /// <summary>
    /// Displays the category name and recursively displays all children.
    /// </summary>
    /// <param name="depth">The indentation depth</param>
    public override void Display(int depth)
    {
        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}+ Category: {Name}");

        foreach (var child in _children)
        {
            child.Display(depth + 1);
        }
    }
}


