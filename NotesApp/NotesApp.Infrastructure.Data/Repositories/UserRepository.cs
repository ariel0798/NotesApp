using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;
using Redis.OM;

namespace NotesApp.Infrastructure.Data.Repositories;

public class UserRepository : BaseRepository<User> , IUserRepository
{
    public UserRepository(RedisConnectionProvider provider) : base(provider)
    {
        provider.Connection.CreateIndex(typeof(User));
    }
}