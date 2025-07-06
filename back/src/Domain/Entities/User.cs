namespace Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public int Age { get; private set; }
    public string Phone { get; private set; }
    public string City { get; private set; }
    public string? PhotoUrl { get; private set; }

    private User() { } // Para Entity Framework

    public User(string name, string email, int age, string phone, string city, string? photoUrl = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Age = age > 0 ? age : throw new ArgumentException("La edad debe ser mayor a 0", nameof(age));
        Phone = phone ?? throw new ArgumentNullException(nameof(phone));
        City = city ?? throw new ArgumentNullException(nameof(city));
        PhotoUrl = photoUrl;
    }

    public void UpdateDetails(string name, string email, int age, string phone, string city)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Age = age > 0 ? age : throw new ArgumentException("La edad debe ser mayor a 0", nameof(age));
        Phone = phone ?? throw new ArgumentNullException(nameof(phone));
        City = city ?? throw new ArgumentNullException(nameof(city));
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePhoto(string photoUrl)
    {
        PhotoUrl = photoUrl ?? throw new ArgumentNullException(nameof(photoUrl));
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemovePhoto()
    {
        PhotoUrl = null;
        UpdatedAt = DateTime.UtcNow;
    }
} 