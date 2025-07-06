namespace Application.DTOs;

public record UserDto(
    Guid Id,
    string Name,
    string Email,
    int Age,
    string Phone,
    string City,
    string? PhotoUrl,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CreateUserDto(
    string Name,
    string Email,
    int Age,
    string Phone,
    string City,
    string? PhotoUrl
);

public record UpdateUserDto(
    string Name,
    string Email,
    int Age,
    string Phone,
    string City
);

public record UpdateUserPhotoDto(
    string PhotoUrl
); 