using System.Linq.Expressions;
using NotesApp.Domain.Models;

namespace NotesApp.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> Create(User obj);
    Task<User?> GetById(string id);
    Task<IEnumerable<User>> GetAll();
    Task Update(User obj);
    Task Remove(User obj);
}