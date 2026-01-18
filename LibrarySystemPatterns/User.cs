namespace LibrarySystemPatterns;

// пользователь библиотеки с именем и ролью
public class User
{
    public string Name { get; }
    public UserRole Role { get; }
    
    public User(string name, UserRole role)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("User name cannot be empty", nameof(name));
        }
        Role = role;
    }
}
