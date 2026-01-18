using LibrarySystemPatterns;

namespace LibrarySystemPatterns.Patterns.Proxy;

// интерфейс для управления библиотекой
public interface ILibraryManagement
{
    // добавить книгу в каталог
    void AddBookToCatalog(string title);

    // оформить книгу
    void CheckoutBook(string title, string userName);

    // вернуть книгу
    void ReturnBook(string title);
}
