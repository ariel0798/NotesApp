using MediatR;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Users.Commands;

public class CreateUserCommand : IRequest<User>
{
    public string Id{ get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } 
    public string PasswordSalt { get; set; } 
    public string RefreshToken { get; set; }
    public DateTime TokenCreated { get; set; } 
    public DateTime TokenExpires { get; set; }
    public DateTime UserCreated { get; set; } 
    public string NoteId { get; set; } 
}