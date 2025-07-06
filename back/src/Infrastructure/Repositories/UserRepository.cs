using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using System.Data;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDatabaseConnection _databaseConnection;

    public UserRepository(IDatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        using var connection = _databaseConnection.CreateConnection();
        var parameters = new { Id = id };
        return await connection.QueryFirstOrDefaultAsync<User>(
            "sp_GetUserById",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = _databaseConnection.CreateConnection();
        return await connection.QueryAsync<User>(
            "sp_GetAllUsers",
            commandType: CommandType.StoredProcedure);
    }

    public async Task<User> AddAsync(User user)
    {
        using var connection = _databaseConnection.CreateConnection();
        var parameters = new
        {
            user.Id,
            user.Name,
            user.Email,
            user.Age,
            user.Phone,
            user.City,
            user.PhotoUrl,
            user.CreatedAt,
            user.UpdatedAt
        };

        await connection.ExecuteAsync(
            "sp_CreateUser",
            parameters,
            commandType: CommandType.StoredProcedure);

        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        using var connection = _databaseConnection.CreateConnection();
        var parameters = new
        {
            user.Id,
            user.Name,
            user.Email,
            user.Age,
            user.Phone,
            user.City,
            user.PhotoUrl,
            user.CreatedAt,
            UpdatedAt = DateTime.UtcNow
        };

        await connection.ExecuteAsync(
            "sp_UpdateUser",
            parameters,
            commandType: CommandType.StoredProcedure);

        return user;
    }

    public async Task DeleteAsync(Guid id)
    {
        using var connection = _databaseConnection.CreateConnection();
        var parameters = new { Id = id };
        await connection.ExecuteAsync(
            "sp_DeleteUser",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        using var connection = _databaseConnection.CreateConnection();
        var parameters = new { Id = id };
        var count = await connection.ExecuteScalarAsync<int>(
            "sp_UserExists",
            parameters,
            commandType: CommandType.StoredProcedure);
        return count > 0;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        using var connection = _databaseConnection.CreateConnection();
        var parameters = new { Email = email };
        return await connection.QueryFirstOrDefaultAsync<User>(
            "sp_GetUserByEmail",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<User>> GetByCityAsync(string city)
    {
        using var connection = _databaseConnection.CreateConnection();
        var parameters = new { City = city };
        return await connection.QueryAsync<User>(
            "sp_GetUsersByCity",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        using var connection = _databaseConnection.CreateConnection();
        var parameters = new { Email = email };
        var count = await connection.ExecuteScalarAsync<int>(
            "sp_EmailExists",
            parameters,
            commandType: CommandType.StoredProcedure);
        return count > 0;
    }

    public async Task<bool> EmailExistsForOtherUserAsync(string email, Guid userId)
    {
        using var connection = _databaseConnection.CreateConnection();
        var parameters = new { Email = email, UserId = userId };
        var count = await connection.ExecuteScalarAsync<int>(
            "sp_EmailExistsForOtherUser",
            parameters,
            commandType: CommandType.StoredProcedure);
        return count > 0;
    }
} 