using MediatR;

namespace NotesApp.ApplicationCore.Users.Queries;

public class GetUserByEmailQuery : IRequest<Domain.Models.User>
{
    public string Email { get; init; }
}