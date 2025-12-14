namespace LibrarySystemPatterns.Patterns.Adapter;

/// <summary>
/// Legacy search system that expects queries in JSON format.
/// This represents an existing system that we need to adapt to our new interface.
/// </summary>
public class OldSearchSystem
{
    /// <summary>
    /// Performs a search using the legacy system with a JSON query.
    /// </summary>
    /// <param name="jsonQuery">The search query in JSON format</param>
    /// <returns>A string containing the search results</returns>
    public string LegacySearch(string jsonQuery)
    {
        // Simulate legacy search functionality
        // In a real scenario, this would interact with an actual legacy system
        return $"Legacy search results for query: {jsonQuery}";
    }
}


