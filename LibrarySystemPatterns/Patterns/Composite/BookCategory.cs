namespace LibrarySystemPatterns.Patterns.Composite;

// категория - это композит в паттерне composite, может содержать книги и другие категории
public class BookCategory : LibraryComponent
{
    private readonly List<LibraryComponent> _children = new();

    public BookCategory(string name) : base(name)
    {
    }

    // добавляем книгу или категорию
    public void Add(LibraryComponent component)
    {
        if (component == null)
            throw new ArgumentNullException(nameof(component));

        _children.Add(component);
    }

    // удаляем компонент
    public bool Remove(LibraryComponent component)
    {
        if (component == null)
            return false;

        return _children.Remove(component);
    }

    // получаем все дочерние элементы
    public IEnumerable<LibraryComponent> GetChildren()
    {
        return _children;
    }

    // показываем категорию и все что внутри
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
