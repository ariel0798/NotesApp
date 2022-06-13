using MediatR;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Users.Queries;

public class GetUserByRefreshTokenQuery : IRequest<User>
{
    public string RefreshToken { get; set; }
}