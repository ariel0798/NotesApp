namespace NotesApp.ApplicationCore.Authentication.Models;

public class JwtToken
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Expires { get; set; }
}