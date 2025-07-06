using Application.DTOs;
using Domain.Repositories;

namespace Application.UseCases.Users;

public class UpdateUserPhotoUseCase : IUpdateUserPhotoUseCase
{
    private readonly IUserRepository _userRepository;

    public UpdateUserPhotoUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> ExecuteAsync(Guid id, UpdateUserPhotoDto updateUserPhotoDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return null;

        user.UpdatePhoto(updateUserPhotoDto.PhotoUrl);

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