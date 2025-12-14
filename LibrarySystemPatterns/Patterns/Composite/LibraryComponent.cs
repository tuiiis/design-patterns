namespace LibrarySystemPatterns.Patterns.Composite;

/// <summary>
/// Abstract base class for the Composite Pattern.
/// Represents both leaf (Book) and composite (BookCategory) components.
/// </summary>
public abstract class LibraryComponent
{
    public string Name { get; protected set; }

    protected LibraryComponent(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Displays the component with the specified indentation depth.
    /// </summary>
    /// <param name="depth">The indentation depth for hierarchical display</param>
    public abstract void Display(int depth);
}


