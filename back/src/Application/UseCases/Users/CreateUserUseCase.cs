using Application.DTOs;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases.Users;

public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IUserRepository _userRepository;

    public CreateUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> ExecuteAsync(CreateUserDto createUserDto)
    {
        // Validar que el email no exista
        if (await _userRepository.EmailExistsAsync(createUserDto.Email))
        {
            throw new InvalidOperationException($"Ya existe un usuario con el email: {createUserDto.Email}");
        }

        var user = new User(
            createUserDto.Name,
            createUserDto.Email,
            createUserDto.Age,
            createUserDto.Phone,
            createUserDto.City,
            createUserDto.PhotoUrl
        );

        var createdUser = await _userRepository.AddAsync(user);
        
        return new UserDto(
            createdUser.Id,
            createdUser.Name,
            createdUser.Email,
            createdUser.Age,
            createdUser.Phone,
            createdUser.City,
            createdUser.PhotoUrl,
            createdUser.CreatedAt,
            createdUser.UpdatedAt
        );
    }
} 