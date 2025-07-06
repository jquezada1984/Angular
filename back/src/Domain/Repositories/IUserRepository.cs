using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetByCityAsync(string city);
    Task<bool> EmailExistsAsync(string email);
    Task<bool> EmailExistsForOtherUserAsync(string email, Guid userId);
} 