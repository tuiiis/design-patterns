using LibrarySystemPatterns.Patterns.Composite;

namespace LibrarySystemPatterns.Patterns.Decorator;

// базовый декоратор - оборачивает компонент библиотеки
public abstract class BookDecorator : LibraryComponent
{
    public LibraryComponent Component { get; }

    protected BookDecorator(LibraryComponent component) : base(component.Name)
    {
        Component = component ?? throw new ArgumentNullException(nameof(component));
    }

    // показываем обернутый компонент
    public override void Display(int depth)
    {
        Component.Display(depth);
    }
}
