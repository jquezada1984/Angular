using Application.DTOs;
using Domain.Repositories;

namespace Application.UseCases.Users;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserRepository _userRepository;

    public UpdateUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> ExecuteAsync(Guid id, UpdateUserDto updateUserDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return null;

        // Validar que el email no exista para otro usuario
        if (await _userRepository.EmailExistsForOtherUserAsync(updateUserDto.Email, id))
        {
            throw new InvalidOperationException($"Ya existe otro usuario con el email: {updateUserDto.Email}");
        }

        user.UpdateDetails(
            updateUserDto.Name,
            updateUserDto.Email,
            updateUserDto.Age,
            updateUserDto.Phone,
            updateUserDto.City
        );

        var updatedUser = await _userRepository.UpdateAsync(user);
        
        return new UserDto(
            updatedUser.Id,
            updatedUser.Name,
            updatedUser.Email,
            updatedUser.Age,
            updatedUser.Phone,
            updatedUser.City,
            updatedUser.PhotoUrl,
            updatedUser.CreatedAt,
            updatedUser.UpdatedAt
        );
    }
} 