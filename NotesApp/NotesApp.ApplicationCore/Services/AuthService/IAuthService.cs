using NotesApp.ApplicationCore.Contracts.User.Requests;
using NotesApp.ApplicationCore.Models;

namespace NotesApp.ApplicationCore.Services.AuthService;

public interface IAuthService
{
    Task<string> RegisterUser(RegisterUserRequest userDto);
    Task<JwtToken?> LoginUser(LoginRequest userDto);
    Task<JwtToken?> RefreshToken(string refreshToken);
    string GetUserEmail();
}