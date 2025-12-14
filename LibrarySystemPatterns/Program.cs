// See https://aka.ms/new-console-template for more information
using LibrarySystemPatterns.Patterns.Flyweight;
using LibrarySystemPatterns.Patterns.Composite;
using LibrarySystemPatterns.Patterns.Decorator;
using LibrarySystemPatterns.Patterns.Adapter;
using LibrarySystemPatterns.Patterns.Proxy;
using LibrarySystemPatterns.Patterns.Facade;

Console.WriteLine("=== Flyweight Pattern Demo ===\n");

// Demonstrate the Flyweight Pattern with Author objects
var factory = new AuthorFactory();

// Request the same author multiple times
var author1 = factory.GetAuthor("J.K. Rowling");
var author2 = factory.GetAuthor("J.K. Rowling");
var author3 = factory.GetAuthor("George R.R. Martin");
var author4 = factory.GetAuthor("J.K. Rowling");

// Verify that the same author instances are reused
Console.WriteLine($"Author 1 Name: {author1.Name}");
Console.WriteLine($"Author 2 Name: {author2.Name}");
Console.WriteLine($"Author 1 and Author 2 are the same instance: {ReferenceEquals(author1, author2)}");
Console.WriteLine($"Author 1 and Author 4 are the same instance: {ReferenceEquals(author1, author4)}");
Console.WriteLine($"Author 1 and Author 3 are different instances: {!ReferenceEquals(author1, author3)}");
Console.WriteLine($"Total unique authors in factory: {factory.AuthorCount}");

Console.WriteLine("\n=== Composite Pattern Demo ===\n");

// Create authors using the Flyweight factory
var rowling = factory.GetAuthor("J.K. Rowling");
var martin = factory.GetAuthor("George R.R. Martin");
var tolkien = factory.GetAuthor("J.R.R. Tolkien");

// Create books
var harryPotter1 = new Book("Harry Potter and the Philosopher's Stone", rowling);
var harryPotter2 = new Book("Harry Potter and the Chamber of Secrets", rowling);
var gameOfThrones = new Book("A Game of Thrones", martin);
var hobbit = new Book("The Hobbit", tolkien);
var lotr = new Book("The Lord of the Rings", tolkien);

// Create categories
var fantasyCategory = new BookCategory("Fantasy");
var fictionCategory = new BookCategory("Fiction");
var childrenCategory = new BookCategory("Children's Books");

// Build the composite structure
fantasyCategory.Add(harryPotter1);
fantasyCategory.Add(harryPotter2);
fantasyCategory.Add(gameOfThrones);
fantasyCategory.Add(hobbit);
fantasyCategory.Add(lotr);

childrenCategory.Add(harryPotter1);
childrenCategory.Add(harryPotter2);
childrenCategory.Add(hobbit);

fictionCategory.Add(fantasyCategory);
fictionCategory.Add(childrenCategory);

// Display the entire structure
Console.WriteLine("Library Structure:");
Console.WriteLine("==================");
fictionCategory.Display(0);

Console.WriteLine($"\nTotal unique authors in factory: {factory.AuthorCount}");

Console.WriteLine("\n=== Decorator Pattern Demo ===\n");

// Demonstrate the Decorator Pattern by adding ratings to books dynamically
var book1 = new Book("The Great Gatsby", factory.GetAuthor("F. Scott Fitzgerald"));
var book2 = new Book("1984", factory.GetAuthor("George Orwell"));
var book3 = new Book("To Kill a Mockingbird", factory.GetAuthor("Harper Lee"));

// Decorate books with ratings
var ratedBook1 = new RatingDecorator(book1, 4.5);
var ratedBook2 = new RatingDecorator(book2, 4.8);
var ratedBook3 = new RatingDecorator(book3, 4.7);

Console.WriteLine("Books with Ratings:");
Console.WriteLine("===================");
ratedBook1.Display(0);
Console.WriteLine();
ratedBook2.Display(0);
Console.WriteLine();
ratedBook3.Display(0);

// You can also decorate already decorated books (stacking decorators)
// For example, if we had multiple decorators, we could stack them
Console.WriteLine("\nOriginal book (without rating):");
book1.Display(0);

Console.WriteLine("\n=== Adapter Pattern Demo ===\n");

// Demonstrate the Adapter Pattern by integrating the legacy search system
var oldSearchSystem = new OldSearchSystem();

// Create an adapter to use the legacy system with the new interface
ISearchSystem searchSystem = new SearchSystemAdapter(oldSearchSystem);

// Use the new interface with simple string queries
Console.WriteLine("Searching with simple string queries:");
Console.WriteLine("====================================");
searchSystem.Search("Harry Potter");
Console.WriteLine();
searchSystem.Search("fantasy books");
Console.WriteLine();
searchSystem.Search("J.K. Rowling");

// Show that the adapter converts simple queries to JSON format
Console.WriteLine("\nNote: The adapter automatically converts simple string queries");
Console.WriteLine("to JSON format required by the legacy system.");

Console.WriteLine("\n=== Proxy Pattern Demo ===\n");

// Demonstrate the Proxy Pattern for access control
Console.WriteLine("Attempting to add books with different user roles:");
Console.WriteLine("=================================================");

// Create proxy with Admin role
ILibraryManagement adminManager = new LibraryManagerProxy("Admin");
Console.WriteLine("\n[Admin User]");
adminManager.AddBookToCatalog("The Catcher in the Rye");
adminManager.AddBookToCatalog("Pride and Prejudice");

// Create proxy with User role (non-admin)
ILibraryManagement regularUserManager = new LibraryManagerProxy("User");
Console.WriteLine("\n[Regular User]");
regularUserManager.AddBookToCatalog("Moby Dick");

// Create proxy with Librarian role (non-admin)
ILibraryManagement librarianManager = new LibraryManagerProxy("Librarian");
Console.WriteLine("\n[Librarian]");
librarianManager.AddBookToCatalog("War and Peace");

// Show direct access to real implementation (bypassing proxy)
Console.WriteLine("\n[Direct Access to RealLibraryManagement - No Proxy]");
var realManager = new RealLibraryManagement();
realManager.AddBookToCatalog("Direct Access Book");

Console.WriteLine("\n=== Facade Pattern - System Test ===\n");

// Test the system using the Facade Pattern
Console.WriteLine("Testing the Library System through LibraryFacade:");
Console.WriteLine("===============================================\n");

// 1. Create an instance with role "User" and try to add a book (should fail)
Console.WriteLine("1. Testing User Role (Access Denied):");
Console.WriteLine("------------------------------------");
var userFacade = new LibraryFacade("User");
userFacade.AddBook("Fantasy", "The Hobbit", "J.R.R. Tolkien");
Console.WriteLine();

// 2. Create an instance with role "Admin"
Console.WriteLine("2. Creating Admin Facade:");
Console.WriteLine("------------------------");
var adminFacade = new LibraryFacade("Admin");
Console.WriteLine("Admin facade created successfully.\n");

// 3. Add several books using the Admin facade (demonstrating Flyweight and Composite)
Console.WriteLine("3. Adding Books (Flyweight + Composite Patterns):");
Console.WriteLine("-------------------------------------------------");
adminFacade.AddBook("Fantasy", "The Name of the Wind", "Patrick Rothfuss");
adminFacade.AddBook("Science Fiction", "Dune", "Frank Herbert");
adminFacade.AddBook("Fantasy", "The Way of Kings", "Brandon Sanderson");
adminFacade.AddBook("Fantasy", "The Wise Man's Fear", "Patrick Rothfuss"); // Same author - Flyweight pattern
adminFacade.AddBook("Mystery", "The Girl with the Dragon Tattoo", "Stieg Larsson");
Console.WriteLine();

// 4. Search for a book (demonstrating Adapter)
Console.WriteLine("4. Searching for Books (Adapter Pattern):");
Console.WriteLine("------------------------------------------");
adminFacade.Search("fantasy books");
Console.WriteLine();

// 5. Display the catalog
Console.WriteLine("5. Displaying Catalog (Composite Pattern):");
Console.WriteLine("------------------------------------------");
adminFacade.ShowCatalog();
Console.WriteLine();

// 6. Apply a rating to a book (demonstrating Decorator)
Console.WriteLine("6. Rating a Book (Decorator Pattern):");
Console.WriteLine("------------------------------------");
adminFacade.RateBook("The Name of the Wind", 4.8);
Console.WriteLine();
adminFacade.RateBook("Dune", 4.9);
Console.WriteLine();

// Show the catalog again to see the ratings
Console.WriteLine("Updated Catalog with Ratings:");
Console.WriteLine("-----------------------------");
adminFacade.ShowCatalog();
