using Application.DTOs;

namespace Application.UseCases.Users;

public interface IGetUserUseCase
{
    Task<UserDto?> GetByIdAsync(Guid id);
    Task<UserDto?> GetByEmailAsync(string email);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<IEnumerable<UserDto>> GetByCityAsync(string city);
} 