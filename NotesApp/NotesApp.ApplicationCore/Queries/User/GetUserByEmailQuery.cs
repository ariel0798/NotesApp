using MediatR;

namespace NotesApp.ApplicationCore.Queries.User;

public record GetUserByEmailQuery (string email) : IRequest<Domain.Models.User>
{
}