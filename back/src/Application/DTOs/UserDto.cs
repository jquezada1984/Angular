using Microsoft.AspNetCore.Http.Features;

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
    Stream? PhotoStream,
    string? PhotoFileName,
    string? PhotoContentType
);

public record UpdateUserDto(
    string Name,
    string Email,
    int Age,
    string Phone,
    string City
);

public record UpdateUserPhotoDto(
    Stream PhotoStream,
    string PhotoFileName,
    string PhotoContentType
); 