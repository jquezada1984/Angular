using Domain.Repositories;

namespace Application.UseCases.Users;

public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly IUserRepository _userRepository;

    public DeleteUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        if (!await _userRepository.ExistsAsync(id))
            return false;

        await _userRepository.DeleteAsync(id);
        return true;
    }
} 