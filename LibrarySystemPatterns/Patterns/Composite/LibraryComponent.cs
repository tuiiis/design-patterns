namespace LibrarySystemPatterns.Patterns.Composite;

// базовый класс для паттерна composite - может быть и книгой, и категорией
public abstract class LibraryComponent
{
    public string Name { get; protected set; }

    protected LibraryComponent(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    // отображение компонента с отступом
    public abstract void Display(int depth);
}
