using Application.DTOs;

namespace Application.UseCases.Users;

public interface IUpdateUserUseCase
{
    Task<UserDto?> ExecuteAsync(Guid id, UpdateUserDto updateUserDto);
} 