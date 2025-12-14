using LibrarySystemPatterns.Patterns.Composite;

namespace LibrarySystemPatterns.Patterns.Decorator;

/// <summary>
/// Abstract decorator class for the Decorator Pattern.
/// Wraps a LibraryComponent to add functionality dynamically.
/// </summary>
public abstract class BookDecorator : LibraryComponent
{
    public LibraryComponent Component { get; }

    protected BookDecorator(LibraryComponent component) : base(component.Name)
    {
        Component = component ?? throw new ArgumentNullException(nameof(component));
    }

    /// <summary>
    /// Displays the wrapped component. Subclasses can override to add additional behavior.
    /// </summary>
    /// <param name="depth">The indentation depth</param>
    public override void Display(int depth)
    {
        Component.Display(depth);
    }
}


