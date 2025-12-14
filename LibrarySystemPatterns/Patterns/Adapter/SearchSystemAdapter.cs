using System.Text.Json;

namespace LibrarySystemPatterns.Patterns.Adapter;

/// <summary>
/// Adapter class that bridges the gap between the new ISearchSystem interface
/// and the legacy OldSearchSystem. Converts simple string queries to JSON format.
/// </summary>
public class SearchSystemAdapter : ISearchSystem
{
    private readonly OldSearchSystem _oldSearchSystem;

    public SearchSystemAdapter(OldSearchSystem oldSearchSystem)
    {
        _oldSearchSystem = oldSearchSystem ?? throw new ArgumentNullException(nameof(oldSearchSystem));
    }

    /// <summary>
    /// Performs a search by converting the simple string query to JSON format
    /// and passing it to the legacy search system.
    /// </summary>
    /// <param name="query">The search query as a simple string</param>
    public void Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            Console.WriteLine("Error: Query cannot be empty.");
            return;
        }

        // Convert the simple string query to JSON format
        var jsonQuery = ConvertToJson(query);

        // Call the legacy system
        string results = _oldSearchSystem.LegacySearch(jsonQuery);

        // Display the results
        Console.WriteLine(results);
    }

    /// <summary>
    /// Converts a simple string query to JSON format expected by the legacy system.
    /// </summary>
    /// <param name="query">The simple string query</param>
    /// <returns>JSON formatted query string</returns>
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


