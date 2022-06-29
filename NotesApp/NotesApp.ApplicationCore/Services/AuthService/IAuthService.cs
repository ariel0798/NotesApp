using NotesApp.ApplicationCore.Contracts.User.Requests;
using NotesApp.ApplicationCore.Models;

namespace NotesApp.ApplicationCore.Services.AuthService;

public interface IAuthService
{
    Task<string> RegisterUser(RegisterUserRequest registerUserRequest);
    Task<JwtToken?> LoginUser(LoginRequest loginRequest);
    Task<JwtToken?> RefreshToken(string refreshToken);
    string GetUserEmail();
}