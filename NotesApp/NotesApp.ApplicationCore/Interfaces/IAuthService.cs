using NotesApp.ApplicationCore.Dtos.User;
using NotesApp.ApplicationCore.Models;

namespace NotesApp.ApplicationCore.Interfaces;

public interface IAuthService
{
    Task<string> RegisterUser(RegisterUserDto userDto);
    Task<JwtToken?> LoginUser(LoginUserDto userDto);
    Task<JwtToken?> RefreshToken(string email, string refreshToken);
}