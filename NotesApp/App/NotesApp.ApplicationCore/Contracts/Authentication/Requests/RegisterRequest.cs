namespace NotesApp.ApplicationCore.Contracts.Authentication.Requests;

public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
}