using Application.DTOs;
using Domain.Repositories;

namespace Application.UseCases.Users;

public class GetUserUseCase : IGetUserUseCase
{
    private readonly IUserRepository _userRepository;

    public GetUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user != null ? MapToDto(user) : null;
    }

    public async Task<UserDto?> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        return user != null ? MapToDto(user) : null;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(MapToDto);
    }

    public async Task<IEnumerable<UserDto>> GetByCityAsync(string city)
    {
        var users = await _userRepository.GetByCityAsync(city);
        return users.Select(MapToDto);
    }

    private static UserDto MapToDto(Domain.Entities.User user)
    {
        return new UserDto(
            user.Id,
            user.Name,
            user.Email,
            user.Age,
            user.Phone,
            user.City,
            user.PhotoUrl,
            user.CreatedAt,
            user.UpdatedAt
        );
    }
} 