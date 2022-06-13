using NotesApp.ApplicationCore.Dtos.User;
using NotesApp.ApplicationCore.Models;

namespace NotesApp.ApplicationCore.Services.AuthService;

public interface IAuthService
{
    Task<string> RegisterUser(RegisterUserDto userDto);
    Task<JwtToken?> LoginUser(LoginUserDto userDto);
    Task<JwtToken?> RefreshToken(string refreshToken);
}