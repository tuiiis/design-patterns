using LibrarySystemPatterns.Patterns.Flyweight;

namespace LibrarySystemPatterns.Patterns.Composite;

// книга - это лист в паттерне composite
public class Book : LibraryComponent
{
    public Author Author { get; }
    public string Title => Name;
    public bool IsCheckedOut { get; set; }
    public string? CheckedOutBy { get; set; }

    public Book(string title, Author author) : base(title)
    {
        Author = author ?? throw new ArgumentNullException(nameof(author));
        IsCheckedOut = false;
        CheckedOutBy = null;
    }

    // показываем книгу с отступом
    public override void Display(int depth)
    {
        string indent = new string(' ', depth * 2);
        string status = IsCheckedOut 
            ? $" [Checked Out: {CheckedOutBy}]" 
            : " [Available]";
        Console.WriteLine($"{indent}- Book: {Title}, Author: {Author.Name}{status}");
    }
}
