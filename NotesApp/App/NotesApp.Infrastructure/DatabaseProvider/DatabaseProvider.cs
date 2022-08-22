using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace NotesApp.Infrastructure.DatabaseProvider;

public class DatabaseProvider : IDatabaseProvider
{
    private readonly IConfiguration _config;
    
    public DatabaseProvider(IConfiguration configuration)
    {
        _config = configuration;
    }

    public SqlConnection GetConnection()
    {
        return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
    }
}