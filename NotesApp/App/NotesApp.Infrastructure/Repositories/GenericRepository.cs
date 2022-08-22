using Microsoft.EntityFrameworkCore;
using NotesApp.Domain.Interfaces;
using NotesApp.Infrastructure.Context;

namespace NotesApp.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T>
where T : class
{
    private readonly DbSet<T> _table;
    
    public GenericRepository(NotesAppDbContext context)
    {
        _table = context.Set<T>();
    }

    public  async Task<T> Add(T entity)
    {
        await _table.AddAsync(entity);
        return entity;
    }

    public void Update(T entity)
    {
        _table.Update(entity);
    }

    public void Delete(T entity)
    {
        _table.Remove(entity);
    }
}