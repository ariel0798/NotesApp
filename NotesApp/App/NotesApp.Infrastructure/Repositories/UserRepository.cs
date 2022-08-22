using Dapper;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.Infrastructure.Context;
using NotesApp.Infrastructure.DatabaseProvider;

namespace NotesApp.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User> , IUserRepository
{
    private readonly IDatabaseProvider _databaseProvider;
    public UserRepository(NotesAppDbContext context, IDatabaseProvider databaseProvider) 
        : base(context)
    {
        _databaseProvider = databaseProvider;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        await using var connection = _databaseProvider.GetConnection();
        
        var query = "SELECT * FROM [User] WHERE Email = @Email";
        
        return  await connection.QueryFirstOrDefaultAsync<User>(query, new {Email = email});
    }

    public async Task<User> GetUserByRefreshToken(string refreshToken)
    {
        await using var connection = _databaseProvider.GetConnection();
        
        var query = "SELECT * FROM [User] WHERE RefreshToken = @RefreshToken";
        
        return  await connection.QueryFirstOrDefaultAsync<User>(query, new {RefreshToken = refreshToken });
    }
}