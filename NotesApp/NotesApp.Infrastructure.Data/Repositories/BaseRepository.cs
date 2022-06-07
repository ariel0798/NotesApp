using System.Linq.Expressions;
using NotesApp.Domain.Interfaces;
using Redis.OM;
using Redis.OM.Searching;

namespace NotesApp.Infrastructure.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly RedisCollection<T> Collection;
    
    public BaseRepository(RedisConnectionProvider provider)
    {
        Collection = (RedisCollection<T>)provider.RedisCollection<T>();
    }
    public async Task<T> Create(T obj)
    {
        await Collection.InsertAsync(obj);
        return obj;
    }

    public async Task<T?> GetById(string id)
    {
        return await Collection.FindByIdAsync(id);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await Collection.ToListAsync();
    }

    public async Task<bool> Any(Expression<Func<T, bool>> expression)
    {
        return await Collection.AnyAsync(expression);
    }

    public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return await Collection.Where(expression).ToListAsync();
    }

    public async Task<T?> FindFirstByCondition(Expression<Func<T, bool>> expression)
    {
        return await Collection.FirstOrDefaultAsync(expression);
    }

    public async Task Update(T obj)
    {
        await Collection.Update(obj);
    }

    public async Task Remove(T obj)
    {
        await Collection.Delete(obj);
    }
}