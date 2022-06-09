using NotesApp.ApplicationCore.Models;

namespace NotesApp.ApplicationCore.Helper.Interfaces;

public interface IJwtHelper
{
    string CreateToken(string email);
    JwtToken GenerateRefreshToken();
}