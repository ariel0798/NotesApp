using LanguageExt.Common;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.ApplicationCore.Contracts.Authentication.Requests;

namespace NotesApp.ApplicationCore.Services.AuthService;

public interface IAuthService
{
    Task<Result<bool>>RegisterUser(RegisterUserRequest registerUserRequest);
    Task<Result<JwtToken>> LoginUser(LoginRequest loginRequest);
    Task<Result<JwtToken>> RefreshToken(string refreshToken);
    string GetUserEmail();
}