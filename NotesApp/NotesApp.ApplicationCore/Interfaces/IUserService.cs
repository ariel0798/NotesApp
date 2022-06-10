using NotesApp.ApplicationCore.Dtos.User;
using NotesApp.ApplicationCore.Models;

namespace NotesApp.ApplicationCore.Interfaces;

public interface IUserService
{
    Task<string> RegisterUser(RegisterUserDto userDto);
    Task<JwtToken?> LoginUser(LoginUserDto userDto);
}