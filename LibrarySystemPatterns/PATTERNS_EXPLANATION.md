# Design Patterns Explanation

This document explains each design pattern in theory and how it's implemented in the Library System codebase.

---

## 1. Adapter Pattern

### Theory

The **Adapter Pattern** is a structural design pattern that allows objects with incompatible interfaces to work together. It acts as a bridge between two incompatible interfaces by wrapping an object and translating calls to a format the wrapped object understands.

**Key Components:**
- **Target Interface**: The interface that the client expects to use
- **Adaptee**: The existing class with an incompatible interface
- **Adapter**: The class that bridges the gap between Target and Adaptee

**When to use:**
- When you need to use an existing class whose interface doesn't match what you need
- When you want to integrate third-party libraries with incompatible interfaces
- When you need to create a reusable class that works with unrelated classes

### Implementation in Code

In this library system, the Adapter pattern is used to integrate an old search system that only accepts JSON queries with a new interface that accepts simple string queries.

**Files:**
- `ISearchSystem.cs` - The target interface (what the client expects)
- `OldSearchSystem.cs` - The adaptee (existing incompatible system)
- `SearchSystemAdapter.cs` - The adapter (bridges the gap)

**How it works:**

1. **Target Interface** (`ISearchSystem`):
```csharp
public interface ISearchSystem
{
    void Search(string query);  // Simple string query
}
```

2. **Adaptee** (`OldSearchSystem`):
```csharp
public class OldSearchSystem
{
    public string LegacySearch(string jsonQuery)  // Requires JSON format
    {
        return $"Search results for query: {jsonQuery}";
    }
}
```

3. **Adapter** (`SearchSystemAdapter`):
```csharp
public class SearchSystemAdapter : ISearchSystem
{
    private readonly OldSearchSystem _oldSearchSystem;
    
    public void Search(string query)
    {
        // Converts simple string to JSON format
        var jsonQuery = ConvertToJson(query);
        string results = _oldSearchSystem.LegacySearch(jsonQuery);
        Console.WriteLine(results);
    }
}
```

The adapter converts the simple string query into JSON format before passing it to the old system, allowing the new interface to work seamlessly with the legacy system.

---

## 2. Composite Pattern

### Theory

The **Composite Pattern** is a structural design pattern that allows you to compose objects into tree structures to represent part-whole hierarchies. It lets clients treat individual objects and compositions of objects uniformly.

**Key Components:**
- **Component**: Abstract base class/interface that defines common operations
- **Leaf**: Represents individual objects (cannot have children)
- **Composite**: Represents containers that can hold other components (leaves or other composites)

**When to use:**
- When you need to represent part-whole hierarchies
- When you want clients to treat individual objects and compositions uniformly
- When you need to build tree structures

### Implementation in Code

In this library system, the Composite pattern is used to represent the library catalog structure where categories can contain books and other categories.

**Files:**
- `LibraryComponent.cs` - The abstract component base class
- `Book.cs` - The leaf (individual book)
- `BookCategory.cs` - The composite (category that can contain books and other categories)

**How it works:**

1. **Component** (`LibraryComponent`):
```csharp
public abstract class LibraryComponent
{
    public string Name { get; protected set; }
    public abstract void Display(int depth);  // Common operation
}
```

2. **Leaf** (`Book`):
```csharp
public class Book : LibraryComponent
{
    public Author Author { get; }
    public bool IsCheckedOut { get; set; }
    
    public override void Display(int depth)
    {
        // Displays the book with indentation
        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}- Book: {Title}, Author: {Author.Name}");
    }
}
```

3. **Composite** (`BookCategory`):
```csharp
public class BookCategory : LibraryComponent
{
    private readonly List<LibraryComponent> _children = new();
    
    public void Add(LibraryComponent component) { ... }
    public void Remove(LibraryComponent component) { ... }
    
    public override void Display(int depth)
    {
        // Displays category name
        Console.WriteLine($"{indent}+ Category: {Name}");
        // Recursively displays all children
        foreach (var child in _children)
        {
            child.Display(depth + 1);
        }
    }
}
```

This allows the system to build hierarchical structures like:
```
Fiction
  └── Fantasy
      ├── Book: Harry Potter...
      ├── Book: Game of Thrones...
      └── Sci-Fi
          └── Book: Dune...
```

---

## 3. Decorator Pattern

### Theory

The **Decorator Pattern** is a structural design pattern that allows you to attach new behaviors to objects by placing them inside wrapper objects. It provides a flexible alternative to subclassing for extending functionality.

**Key Components:**
- **Component**: The interface/abstract class that defines the object being decorated
- **Concrete Component**: The base object that can be decorated
- **Decorator**: Abstract class that wraps a component
- **Concrete Decorator**: Specific decorator that adds behavior

**When to use:**
- When you need to add responsibilities to objects dynamically
- When you want to add features without modifying existing code
- When subclassing would result in an explosion of classes

### Implementation in Code

In this library system, the Decorator pattern is used to add rating information to books without modifying the Book class itself.

**Files:**
- `BookDecorator.cs` - The abstract decorator base class
- `RatingDecorator.cs` - Concrete decorator that adds rating functionality

**How it works:**

1. **Component**: Uses `LibraryComponent` from Composite pattern (books are components)

2. **Base Decorator** (`BookDecorator`):
```csharp
public abstract class BookDecorator : LibraryComponent
{
    public LibraryComponent Component { get; }
    
    protected BookDecorator(LibraryComponent component) : base(component.Name)
    {
        Component = component;
    }
    
    public override void Display(int depth)
    {
        Component.Display(depth);  // Delegates to wrapped component
    }
}
```

3. **Concrete Decorator** (`RatingDecorator`):
```csharp
public class RatingDecorator : BookDecorator
{
    private readonly double _rating;
    
    public RatingDecorator(LibraryComponent component, double rating) : base(component)
    {
        _rating = rating;
    }
    
    public override void Display(int depth)
    {
        base.Display(depth);  // Display the book first
        // Then add rating information
        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}  *** Rating: {_rating:F1} ***");
    }
}
```

**Usage:**
```csharp
var book = new Book("The Great Gatsby", author);
var ratedBook = new RatingDecorator(book, 4.5);
ratedBook.Display(0);
// Output:
// - Book: The Great Gatsby, Author: F. Scott Fitzgerald
//   *** Rating: 4.5 ***
```

This allows adding rating functionality to books without modifying the Book class, and you can add more decorators (e.g., `ReviewDecorator`, `PriceDecorator`) without creating a class explosion.

---

## 4. Facade Pattern

### Theory

The **Facade Pattern** is a structural design pattern that provides a simplified interface to a complex subsystem. It defines a higher-level interface that makes the subsystem easier to use.

**Key Components:**
- **Facade**: Provides a simple interface to complex subsystem
- **Subsystem Classes**: The complex classes that the facade coordinates

**When to use:**
- When you want to provide a simple interface to a complex subsystem
- When you want to decouple clients from subsystem components
- When you want to layer your subsystems

### Implementation in Code

In this library system, the Facade pattern provides a unified, simplified interface to all the library operations, hiding the complexity of managing multiple subsystems.

**Files:**
- `LibraryFacade.cs` - The facade that simplifies library operations
- `SharedLibraryCatalog.cs` - Singleton that provides shared catalog access

**How it works:**

The `LibraryFacade` class coordinates multiple subsystems:
- **AuthorFactory** (Flyweight pattern)
- **BookCategory** (Composite pattern)
- **SearchSystemAdapter** (Adapter pattern)
- **LibraryManagerProxy** (Proxy pattern)

**Key Methods:**

1. **AddBook**: Coordinates author creation (Flyweight), book creation, and category management (Composite)
```csharp
public void AddBook(string category, string title, string author)
{
    // Uses Proxy for access control
    _libraryManagement.AddBookToCatalog(title);
    
    // Uses Flyweight to get/reuse author
    var authorObj = _authorFactory.GetAuthor(author);
    
    // Uses Composite to add to category
    var book = new Book(title, authorObj);
    var targetCategory = FindOrCreateCategory(category);
    targetCategory.Add(book);
}
```

2. **Search**: Uses the Adapter pattern transparently
```csharp
public void Search(string query)
{
    _searchSystem.Search(query);  // Client doesn't know about adapter
}
```

3. **RateBook**: Uses Decorator pattern to add rating
```csharp
public void RateBook(string title, double rating)
{
    // Finds book, wraps it with RatingDecorator
    var ratedBook = new RatingDecorator(originalBook, rating);
    parentCategory.Remove(bookComponent);
    parentCategory.Add(ratedBook);
}
```

4. **CheckoutBook/ReturnBook**: Uses Proxy pattern for access control
```csharp
public void CheckoutBook(string title)
{
    _libraryManagement.CheckoutBook(title, _user.Name);
}
```

**Benefits:**
- Clients only need to know about `LibraryFacade`, not all the individual patterns
- Simplifies complex operations into single method calls
- Makes the system easier to use and maintain

---

## 5. Flyweight Pattern

### Theory

The **Flyweight Pattern** is a structural design pattern that minimizes memory usage by sharing as much data as possible with similar objects. It's useful when you need to create a large number of similar objects.

**Key Components:**
- **Flyweight**: Interface/class that defines the shared state
- **Concrete Flyweight**: Implements the flyweight interface and stores intrinsic (shared) state
- **Flyweight Factory**: Creates and manages flyweight objects, ensuring they are shared properly
- **Client**: Uses flyweight objects

**Intrinsic vs Extrinsic State:**
- **Intrinsic**: Shared state that can be stored in the flyweight (e.g., author name)
- **Extrinsic**: State that varies and cannot be shared (e.g., book title, checkout status)

**When to use:**
- When you need to create a large number of similar objects
- When object creation is expensive
- When memory usage is a concern

### Implementation in Code

In this library system, the Flyweight pattern is used to share Author objects across multiple books, since many books can have the same author.

**Files:**
- `Author.cs` - The flyweight object (stores intrinsic state - author name)
- `AuthorFactory.cs` - The flyweight factory (manages and reuses Author instances)

**How it works:**

1. **Flyweight** (`Author`):
```csharp
public class Author
{
    public string Name { get; }  // Intrinsic state (shared)
    
    public Author(string name)
    {
        Name = name;
    }
}
```

2. **Flyweight Factory** (`AuthorFactory`):
```csharp
public class AuthorFactory
{
    private readonly Dictionary<string, Author> _authors = new();
    
    public Author GetAuthor(string name)
    {
        // If author exists, return existing instance
        if (!_authors.ContainsKey(name))
        {
            _authors[name] = new Author(name);
        }
        
        return _authors[name];  // Returns shared instance
    }
}
```

**Usage:**
```csharp
var factory = new AuthorFactory();
var author1 = factory.GetAuthor("J.K. Rowling");
var author2 = factory.GetAuthor("J.K. Rowling");

// author1 and author2 are the SAME object instance
Console.WriteLine(ReferenceEquals(author1, author2));  // True
```

**Benefits:**
- Multiple books by the same author share one Author object
- Reduces memory usage when there are many books
- The factory ensures only one Author instance exists per author name

**Note:** The extrinsic state (book title, checkout status) is stored in the `Book` class, not in the `Author` flyweight.

---

## 6. Proxy Pattern

### Theory

The **Proxy Pattern** is a structural design pattern that provides a surrogate or placeholder for another object to control access to it. The proxy acts as an intermediary between the client and the real object.

**Key Components:**
- **Subject**: Interface that both Proxy and RealSubject implement
- **RealSubject**: The actual object that performs the work
- **Proxy**: Controls access to RealSubject, may add additional functionality

**Types of Proxies:**
- **Virtual Proxy**: Creates expensive objects on demand
- **Protection Proxy**: Controls access to the real object
- **Remote Proxy**: Represents an object in a different address space
- **Smart Reference**: Adds additional functionality (logging, caching, etc.)

**When to use:**
- When you need to control access to an object
- When you need to add functionality before/after accessing an object
- When you want to delay expensive object creation

### Implementation in Code

In this library system, the Proxy pattern is used as a **Protection Proxy** to control access to library management operations based on user roles.

**Files:**
- `ILibraryManagement.cs` - The subject interface
- `RealLibraryManagement.cs` - The real subject (actual implementation)
- `LibraryManagerProxy.cs` - The proxy (controls access based on user roles)

**How it works:**

1. **Subject Interface** (`ILibraryManagement`):
```csharp
public interface ILibraryManagement
{
    void AddBookToCatalog(string title);
    void CheckoutBook(string title, string userName);
    void ReturnBook(string title);
}
```

2. **Real Subject** (`RealLibraryManagement`):
```csharp
public class RealLibraryManagement : ILibraryManagement
{
    private readonly BookCategory _catalog;
    
    public void AddBookToCatalog(string title) { ... }
    public void CheckoutBook(string title, string userName) { ... }
    public void ReturnBook(string title) { ... }
}
```

3. **Proxy** (`LibraryManagerProxy`):
```csharp
public class LibraryManagerProxy : ILibraryManagement
{
    private readonly RealLibraryManagement _realLibraryManagement;
    private readonly User _user;
    
    public void AddBookToCatalog(string title)
    {
        // Access control: Only admins can add books
        if (_user.Role == UserRole.Admin)
        {
            _realLibraryManagement.AddBookToCatalog(title);
        }
        else
        {
            Console.WriteLine("Access Denied: Only administrators can add books");
        }
    }
    
    public void CheckoutBook(string title, string userName)
    {
        // Access control: Users and admins can checkout
        if (_user.Role == UserRole.User || _user.Role == UserRole.Admin)
        {
            // Security: Regular users can only checkout as themselves
            if (_user.Role == UserRole.User && userName != _user.Name)
            {
                Console.WriteLine("Access Denied: Users can only checkout books as themselves");
                return;
            }
            
            _realLibraryManagement.CheckoutBook(title, userName);
        }
        else
        {
            Console.WriteLine($"Access Denied: Role '{_user.Role}' cannot checkout books");
        }
    }
}
```

**Benefits:**
- **Access Control**: The proxy enforces security rules before delegating to the real object
- **Separation of Concerns**: Access control logic is separated from business logic
- **Transparent**: Clients use the same interface, unaware they're talking to a proxy
- **Flexible**: Can add logging, caching, or other cross-cutting concerns

**Usage:**
```csharp
var adminUser = new User("Admin", UserRole.Admin);
var regularUser = new User("John", UserRole.User);

var adminManager = new LibraryManagerProxy(adminUser, catalog);
adminManager.AddBookToCatalog("War and Peace");  // ✅ Allowed

var regularManager = new LibraryManagerProxy(regularUser, catalog);
regularManager.AddBookToCatalog("Anna Karenina");  // ❌ Access Denied
```

---

## Pattern Interactions

These patterns work together in the library system:

1. **Facade** coordinates all patterns, providing a simple interface
2. **Composite** structures the catalog hierarchy
3. **Flyweight** shares Author objects across books
4. **Decorator** adds rating to books without modifying Book class
5. **Adapter** integrates the old search system
6. **Proxy** controls access to library operations
7. **Singleton** (in `SharedLibraryCatalog`) ensures one shared catalog instance

The `LibraryFacade` demonstrates how these patterns can work together seamlessly to create a robust, maintainable system.
