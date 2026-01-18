namespace LibrarySystemPatterns.Patterns.Flyweight;

// фабрика авторов - переиспользует объекты авторов
public class AuthorFactory
{
    private readonly Dictionary<string, Author> _authors = new();

    // получаем автора - если уже есть, возвращаем существующего
    public Author GetAuthor(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("имя автора не может быть пустым", nameof(name));

        if (!_authors.ContainsKey(name))
        {
            _authors[name] = new Author(name);
        }

        return _authors[name];
    }

    // сколько уникальных авторов в фабрике
    public int AuthorCount => _authors.Count;
}
