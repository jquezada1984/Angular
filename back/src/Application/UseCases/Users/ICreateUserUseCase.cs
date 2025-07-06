using Application.DTOs;

namespace Application.UseCases.Users;

public interface ICreateUserUseCase
{
    Task<UserDto> ExecuteAsync(CreateUserDto createUserDto);
} 