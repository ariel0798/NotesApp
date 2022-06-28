using NotesApp.ApplicationCore.Models;

namespace NotesApp.ApplicationCore.Helpers.Interfaces;

public interface IJwtHelper
{
    string CreateToken(string email);
    JwtToken GenerateRefreshToken();
}