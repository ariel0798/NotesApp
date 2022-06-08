using NotesApp.ApplicationCore.Dtos.User;

namespace NotesApp.ApplicationCore.Interfaces;

public interface IUserService
{
    Task<string> RegisterUser(RegisterUserDto userDto);
}