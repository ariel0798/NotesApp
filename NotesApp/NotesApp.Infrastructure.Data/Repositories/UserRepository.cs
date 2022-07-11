using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;
using Redis.OM;
using Redis.OM.Searching;

namespace NotesApp.Infrastructure.Data.Repositories;

public class UserRepository :  IUserRepository
{
    private readonly RedisCollection<User> _collection;
    public UserRepository(RedisConnectionProvider provider)
    {
        _collection = (RedisCollection<User>)provider.RedisCollection<User>();
        provider.Connection.CreateIndex(typeof(User));
    }
    
    public async Task<User> Create(User obj)
    {
        await _collection.InsertAsync(obj);
        return obj;
    }

    public async Task<User?> GetById(string id)
    {
        return await _collection.FindByIdAsync(id);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _collection.ToListAsync();    
    }

    public async Task Update(User obj)
    {
        await _collection.Update(obj);
        
    }

    public async Task Remove(User obj)
    {
        await _collection.Delete(obj);
    }
}