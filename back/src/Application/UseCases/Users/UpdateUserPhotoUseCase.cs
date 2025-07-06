using Application.DTOs;
using Application.Services;
using Domain.Repositories;

namespace Application.UseCases.Users;

public class UpdateUserPhotoUseCase : IUpdateUserPhotoUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IFileService _fileService;

    public UpdateUserPhotoUseCase(IUserRepository userRepository, IFileService fileService)
    {
        _userRepository = userRepository;
        _fileService = fileService;
    }

    public async Task<UserDto?> ExecuteAsync(Guid id, UpdateUserPhotoDto updateUserPhotoDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return null;

        // Eliminar imagen anterior si existe
        if (!string.IsNullOrEmpty(user.PhotoUrl))
        {
            _fileService.DeleteImage(user.PhotoUrl);
        }

        // Guardar nueva imagen
        var photoUrl = await _fileService.SaveImageAsync(
            updateUserPhotoDto.PhotoStream, 
            updateUserPhotoDto.PhotoFileName, 
            "usuario"
        );
        user.UpdatePhoto(photoUrl);

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