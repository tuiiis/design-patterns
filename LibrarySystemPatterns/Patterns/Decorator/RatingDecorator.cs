using LibrarySystemPatterns.Patterns.Composite;

namespace LibrarySystemPatterns.Patterns.Decorator;

/// <summary>
/// Concrete decorator that adds a rating to a book.
/// Displays the book details followed by the rating.
/// </summary>
public class RatingDecorator : BookDecorator
{
    private readonly double _rating;

    public RatingDecorator(LibraryComponent component, double rating) : base(component)
    {
        if (rating < 0 || rating > 5)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 0 and 5.");

        _rating = rating;
    }

    /// <summary>
    /// Displays the book details followed by the rating.
    /// </summary>
    /// <param name="depth">The indentation depth</param>
    public override void Display(int depth)
    {
        base.Display(depth);
        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}  *** Rating: {_rating:F1} ***");
    }
}


