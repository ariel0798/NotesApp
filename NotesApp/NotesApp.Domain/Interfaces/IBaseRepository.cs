using System.Linq.Expressions;

namespace NotesApp.Domain.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T> Create(T obj);
    Task<T?> GetById(string id);
    Task<IEnumerable<T>> GetAll();
    Task<bool> Any(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression);
    Task<T?> FindFirstByCondition(Expression<Func<T, bool>> expression);
    Task Update(T obj);
    Task Remove(T obj);
}