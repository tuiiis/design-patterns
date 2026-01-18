using LibrarySystemPatterns.Patterns.Composite;

namespace LibrarySystemPatterns.Patterns.Decorator;

// декоратор для добавления рейтинга к книге
public class RatingDecorator : BookDecorator
{
    private readonly double _rating;

    public RatingDecorator(LibraryComponent component, double rating) : base(component)
    {
        if (rating < 0 || rating > 5)
            throw new ArgumentOutOfRangeException(nameof(rating), "рейтинг должен быть от 0 до 5");

        _rating = rating;
    }

    // показываем книгу и ее рейтинг
    public override void Display(int depth)
    {
        base.Display(depth);
        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}  *** Rating: {_rating:F1} ***");
    }
}
