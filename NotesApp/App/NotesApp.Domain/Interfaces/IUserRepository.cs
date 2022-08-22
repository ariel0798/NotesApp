using NotesApp.Domain.Models;

namespace NotesApp.Domain.Interfaces;

public interface IUserRepository: IGenericRepository<User>
{
    Task<User> GetUserByEmail(string email);
    Task<User> GetUserByRefreshToken(string refreshToken);
}