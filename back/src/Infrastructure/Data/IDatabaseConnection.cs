using System.Data;

namespace Infrastructure.Data;

public interface IDatabaseConnection
{
    IDbConnection CreateConnection();
} 