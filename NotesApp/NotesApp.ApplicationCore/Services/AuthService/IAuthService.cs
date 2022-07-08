using LanguageExt.Common;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.ApplicationCore.Contracts.User.Requests;

namespace NotesApp.ApplicationCore.Services.AuthService;

public interface IAuthService
{
    Task<Result<bool>>RegisterUser(RegisterUserRequest registerUserRequest);
    Task<Result<JwtToken>> LoginUser(LoginRequest loginRequest);
    Task<JwtToken?> RefreshToken(string refreshToken);
    string GetUserEmail();
}