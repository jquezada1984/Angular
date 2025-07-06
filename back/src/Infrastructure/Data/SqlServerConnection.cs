using System.Data;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Data;

public class SqlServerConnection : IDatabaseConnection
{
    private readonly string _connectionString;

    public SqlServerConnection(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
} 