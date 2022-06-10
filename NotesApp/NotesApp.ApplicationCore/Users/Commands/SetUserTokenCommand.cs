using MediatR;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Users.Commands;

public class SetUserTokenCommand : IRequest<User>
{
    public string UserId { get; set; }    
    public string RefreshToken { get; set; } 
    public DateTime TokenCreated { get; set; } 
    public DateTime TokenExpires { get; set; } 
}