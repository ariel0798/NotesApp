using Microsoft.AspNetCore.Http;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Services.AuthService;

public interface IAuthService
{
    Task<JwtToken> SetTokenAndRefreshToken(User user);
    void SetRefreshToken(JwtToken refreshToken, HttpContext context);
}