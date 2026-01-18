namespace LibrarySystemPatterns.Patterns.Adapter;

// старая система поиска, которая работает только с JSON
public class OldSearchSystem
{
    // поиск в старой системе - принимает только JSON
    public string LegacySearch(string jsonQuery)
    {
        return $"результаты поиска для запроса: {jsonQuery}";
    }
}
