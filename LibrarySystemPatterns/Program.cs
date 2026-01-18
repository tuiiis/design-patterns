using LibrarySystemPatterns.Patterns.Flyweight;
using LibrarySystemPatterns.Patterns.Composite;
using LibrarySystemPatterns.Patterns.Decorator;
using LibrarySystemPatterns.Patterns.Adapter;
using LibrarySystemPatterns.Patterns.Proxy;
using LibrarySystemPatterns.Patterns.Facade;
using Bogus;
using System.Linq;

Console.WriteLine("=== паттерн flyweight ===\n");

// демонстрация паттерна flyweight - переиспользование объектов авторов
var factory = new AuthorFactory();
var author1 = factory.GetAuthor("J.K. Rowling");
var author2 = factory.GetAuthor("J.K. Rowling");
var author3 = factory.GetAuthor("George R.R. Martin");

Console.WriteLine($"автор 1 и автор 2 - один объект: {ReferenceEquals(author1, author2)}");
Console.WriteLine($"всего уникальных авторов: {factory.AuthorCount}\n");

Console.WriteLine("=== паттерн composite ===\n");

// создаем структуру категорий и книг
var rowling = factory.GetAuthor("J.K. Rowling");
var martin = factory.GetAuthor("George R.R. Martin");
var tolkien = factory.GetAuthor("J.R.R. Tolkien");

var harryPotter1 = new Book("Гарри Поттер и философский камень", rowling);
var harryPotter2 = new Book("Гарри Поттер и тайная комната", rowling);
var gameOfThrones = new Book("Игра престолов", martin);
var hobbit = new Book("Хоббит", tolkien);

var fantasyCategory = new BookCategory("Фантастика");
var fictionCategory = new BookCategory("Художественная литература");

fantasyCategory.Add(harryPotter1);
fantasyCategory.Add(harryPotter2);
fantasyCategory.Add(gameOfThrones);
fantasyCategory.Add(hobbit);

fictionCategory.Add(fantasyCategory);

Console.WriteLine("структура библиотеки:");
fictionCategory.Display(0);
Console.WriteLine();

Console.WriteLine("=== паттерн decorator ===\n");

// добавляем рейтинг к книгам через декоратор
var book1 = new Book("Великий Гэтсби", factory.GetAuthor("F. Scott Fitzgerald"));
var ratedBook1 = new RatingDecorator(book1, 4.5);

ratedBook1.Display(0);
Console.WriteLine();

Console.WriteLine("=== паттерн adapter ===\n");

// адаптируем старую систему поиска к новому интерфейсу
var oldSearchSystem = new OldSearchSystem();
ISearchSystem searchSystem = new SearchSystemAdapter(oldSearchSystem);

searchSystem.Search("фантастика");
Console.WriteLine();

Console.WriteLine("=== паттерн proxy ===\n");

// проверяем контроль доступа через прокси
var proxyDemoCatalog = new BookCategory("Каталог");

var adminManager = new LibraryManagerProxy("Admin", proxyDemoCatalog);
Console.WriteLine("[администратор]");
adminManager.AddBookToCatalog("Война и мир");

var regularUserManager = new LibraryManagerProxy("User", proxyDemoCatalog);
Console.WriteLine("\n[обычный пользователь]");
regularUserManager.AddBookToCatalog("Анна Каренина");
Console.WriteLine();

Console.WriteLine("=== паттерн facade ===\n");

// используем фасад для упрощенной работы с системой
var adminFacade = new LibraryFacade("Admin");

// добавляем книги через bogus (демонстрация flyweight и composite)
var faker = new Faker();
var categories = new[] { "Фантастика", "Детектив", "Роман", "Триллер" };

Console.WriteLine("добавляем книги через bogus...\n");

for (int i = 0; i < 10; i++)
{
    var authorName = $"{faker.Name.FirstName()} {faker.Name.LastName()}";
    var words = faker.Lorem.Words(faker.Random.Int(2, 3));
    var bookTitle = string.Join(" ", words.Select(w => char.ToUpper(w[0]) + w.Substring(1)));
    var category = faker.PickRandom(categories);
    
    adminFacade.AddBook(category, bookTitle, authorName);
}

Console.WriteLine($"\nдобавлено 10 книг\n");

// поиск через адаптер
adminFacade.Search("фантастика");
Console.WriteLine();

// показываем каталог
adminFacade.ShowCatalog();
Console.WriteLine();

// оформление и возврат через прокси
var userFacade = new LibraryFacade("User");
Console.WriteLine("[пользователь оформляет книгу]");
userFacade.CheckoutBook("тестовая книга");
Console.WriteLine();
Console.WriteLine("[пользователь возвращает книгу]");
userFacade.ReturnBook("тестовая книга");
Console.WriteLine();
