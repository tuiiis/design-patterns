using LibrarySystemPatterns.Patterns.Composite;

namespace LibrarySystemPatterns.Patterns.Facade;

// синглтон для общего каталога библиотеки
public class SharedLibraryCatalog
{
    private static SharedLibraryCatalog? _instance;
    private static readonly object _lock = new object();
    
    public BookCategory Catalog { get; }
    
    private SharedLibraryCatalog()
    {
        Catalog = new BookCategory("Library Catalog");
    }
    
    public static SharedLibraryCatalog Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= new SharedLibraryCatalog();
                }
            }
            return _instance;
        }
    }
}
