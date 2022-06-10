using MediatR;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Users.Queries;

public class GetUserByEmailQuery : IRequest<User>
{
    public string Email { get; init; }
}