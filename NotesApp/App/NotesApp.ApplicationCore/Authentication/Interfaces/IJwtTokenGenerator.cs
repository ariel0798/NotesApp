using NotesApp.ApplicationCore.Authentication.Models;

namespace NotesApp.ApplicationCore.Authentication.Interfaces;

public interface IJwtTokenGenerator
{
    string CreateToken(string email, string userId);
    JwtToken GenerateRefreshToken();
}