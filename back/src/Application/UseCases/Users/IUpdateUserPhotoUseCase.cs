using Application.DTOs;

namespace Application.UseCases.Users;

public interface IUpdateUserPhotoUseCase
{
    Task<UserDto?> ExecuteAsync(Guid id, UpdateUserPhotoDto updateUserPhotoDto);
} 