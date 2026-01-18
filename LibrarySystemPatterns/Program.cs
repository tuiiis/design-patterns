using LibrarySystemPatterns.Patterns.Flyweight;
using LibrarySystemPatterns.Patterns.Composite;
using LibrarySystemPatterns.Patterns.Decorator;
using LibrarySystemPatterns.Patterns.Adapter;
using LibrarySystemPatterns.Patterns.Proxy;
using LibrarySystemPatterns.Patterns.Facade;
using LibrarySystemPatterns;
using Bogus;
using System.Linq;

Console.WriteLine("=== Flyweight Pattern ===\n");

// демонстрация паттерна flyweight - переиспользование объектов авторов
var factory = new AuthorFactory();
var author1 = factory.GetAuthor("J.K. Rowling");
var author2 = factory.GetAuthor("J.K. Rowling");
var author3 = factory.GetAuthor("George R.R. Martin");

Console.WriteLine($"Author 1 and Author 2 are the same object: {ReferenceEquals(author1, author2)}");
Console.WriteLine($"Total unique authors: {factory.AuthorCount}\n");

Console.WriteLine("=== Composite Pattern ===\n");

// создаем структуру категорий и книг
var rowling = factory.GetAuthor("J.K. Rowling");
var martin = factory.GetAuthor("George R.R. Martin");
var tolkien = factory.GetAuthor("J.R.R. Tolkien");

var harryPotter1 = new Book("Harry Potter and the Philosopher's Stone", rowling);
var harryPotter2 = new Book("Harry Potter and the Chamber of Secrets", rowling);
var gameOfThrones = new Book("A Game of Thrones", martin);
var hobbit = new Book("The Hobbit", tolkien);

var fantasyCategory = new BookCategory("Fantasy");
var fictionCategory = new BookCategory("Fiction");

fantasyCategory.Add(harryPotter1);
fantasyCategory.Add(harryPotter2);
fantasyCategory.Add(gameOfThrones);
fantasyCategory.Add(hobbit);

fictionCategory.Add(fantasyCategory);

Console.WriteLine("Library Structure:");
fictionCategory.Display(0);
Console.WriteLine();

Console.WriteLine("=== Decorator Pattern ===\n");

// добавляем рейтинг к книгам через декоратор
var book1 = new Book("The Great Gatsby", factory.GetAuthor("F. Scott Fitzgerald"));
var ratedBook1 = new RatingDecorator(book1, 4.5);

ratedBook1.Display(0);
Console.WriteLine();

Console.WriteLine("=== Adapter Pattern ===\n");

// адаптируем старую систему поиска к новому интерфейсу
var oldSearchSystem = new OldSearchSystem();
ISearchSystem searchSystem = new SearchSystemAdapter(oldSearchSystem);

searchSystem.Search("fantasy");
Console.WriteLine();

Console.WriteLine("=== Proxy Pattern ===\n");

// проверяем контроль доступа через прокси
var proxyDemoCatalog = new BookCategory("Catalog");

var adminManager = new LibraryManagerProxy(UserRole.Admin, proxyDemoCatalog);
Console.WriteLine("[Administrator]");
adminManager.AddBookToCatalog("War and Peace");

var regularUserManager = new LibraryManagerProxy(UserRole.User, proxyDemoCatalog);
Console.WriteLine("\n[Regular User]");
regularUserManager.AddBookToCatalog("Anna Karenina");
Console.WriteLine();

Console.WriteLine("=== Facade Pattern ===\n");

// используем фасад для упрощенной работы с системой
var adminFacade = new LibraryFacade(UserRole.Admin);

// добавляем книги через bogus (демонстрация flyweight и composite)
var faker = new Faker();
var categories = new[] { "Fantasy", "Mystery", "Romance", "Thriller" };

Console.WriteLine("Adding books via Bogus...\n");

for (int i = 0; i < 10; i++)
{
    var authorName = $"{faker.Name.FirstName()} {faker.Name.LastName()}";
    var words = faker.Lorem.Words(faker.Random.Int(2, 3));
    var bookTitle = string.Join(" ", words.Select(w => char.ToUpper(w[0]) + w.Substring(1)));
    var category = faker.PickRandom(categories);
    
    adminFacade.AddBook(category, bookTitle, authorName);
}

Console.WriteLine($"\n10 books added\n");

// поиск через адаптер
adminFacade.Search("fantasy");
Console.WriteLine();

// показываем каталог
adminFacade.ShowCatalog();
Console.WriteLine();

// добавляем конкретную книгу для демонстрации checkout/return
adminFacade.AddBook("Fantasy", "The Hobbit", "J.R.R. Tolkien");
Console.WriteLine();

// оформление и возврат через прокси
// используем adminFacade, так как книга в его каталоге
// прокси проверяет права - админ может оформлять и возвращать книги
Console.WriteLine("[Administrator Checking Out Book]");
adminFacade.CheckoutBook("The Hobbit");
Console.WriteLine();
Console.WriteLine("[Administrator Returning Book]");
adminFacade.ReturnBook("The Hobbit");
Console.WriteLine();
