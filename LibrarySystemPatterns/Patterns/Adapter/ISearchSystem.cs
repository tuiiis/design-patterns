namespace LibrarySystemPatterns.Patterns.Adapter;

// интерфейс для новой системы поиска
public interface ISearchSystem
{
    // поиск по простому текстовому запросу
    void Search(string query);
}
