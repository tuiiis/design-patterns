using System.Text.Json;

namespace LibrarySystemPatterns.Patterns.Adapter;

// адаптер - соединяет новую систему с старой
public class SearchSystemAdapter : ISearchSystem
{
    private readonly OldSearchSystem _oldSearchSystem;

    public SearchSystemAdapter(OldSearchSystem oldSearchSystem)
    {
        _oldSearchSystem = oldSearchSystem ?? throw new ArgumentNullException(nameof(oldSearchSystem));
    }

    // преобразуем простой запрос в JSON и передаем в старую систему
    public void Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            Console.WriteLine("Error: Query cannot be empty");
            return;
        }

        var jsonQuery = ConvertToJson(query);
        string results = _oldSearchSystem.LegacySearch(jsonQuery);
        Console.WriteLine(results);
    }

    // конвертируем строку в JSON формат
    private string ConvertToJson(string query)
    {
        var queryObject = new
        {
            query = query,
            timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
        };

        return JsonSerializer.Serialize(queryObject);
    }
}
