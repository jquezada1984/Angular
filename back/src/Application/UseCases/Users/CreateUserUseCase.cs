using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases.Users;

public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IFileService _fileService;

    public CreateUserUseCase(IUserRepository userRepository, IFileService fileService)
    {
        _userRepository = userRepository;
        _fileService = fileService;
    }

    public async Task<UserDto> ExecuteAsync(CreateUserDto createUserDto)
    {
        // Validar que el email no exista
        if (await _userRepository.EmailExistsAsync(createUserDto.Email))
        {
            throw new InvalidOperationException($"Ya existe un usuario con el email: {createUserDto.Email}");
        }

        string? photoUrl = null;
        
        // Procesar imagen si se proporciona
        if (createUserDto.PhotoStream != null && !string.IsNullOrEmpty(createUserDto.PhotoFileName))
        {
            photoUrl = await _fileService.SaveImageAsync(
                createUserDto.PhotoStream, 
                createUserDto.PhotoFileName, 
                "usuario"
            );
        }

        var user = new User(
            createUserDto.Name,
            createUserDto.Email,
            createUserDto.Age,
            createUserDto.Phone,
            createUserDto.City,
            photoUrl
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