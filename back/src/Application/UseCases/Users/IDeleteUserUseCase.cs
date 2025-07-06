namespace Application.UseCases.Users;

public interface IDeleteUserUseCase
{
    Task<bool> ExecuteAsync(Guid id);
} 