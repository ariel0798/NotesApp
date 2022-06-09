using System.Security.AccessControl;
using MediatR;

namespace NotesApp.ApplicationCore.Queries.User;

public class GetUserByEmailQuery : IRequest<Domain.Models.User>
{
    public string Email { get; init; }
}