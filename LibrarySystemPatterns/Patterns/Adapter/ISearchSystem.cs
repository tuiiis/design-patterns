namespace LibrarySystemPatterns.Patterns.Adapter;

/// <summary>
/// Target interface for the search system.
/// This is the interface that our application expects to use.
/// </summary>
public interface ISearchSystem
{
    /// <summary>
    /// Performs a search with a simple string query.
    /// </summary>
    /// <param name="query">The search query as a simple string</param>
    void Search(string query);
}


