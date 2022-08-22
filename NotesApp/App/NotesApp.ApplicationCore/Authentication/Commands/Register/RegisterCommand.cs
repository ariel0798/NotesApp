using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.User.Responses;

namespace NotesApp.ApplicationCore.Authentication.Commands.Register;

public record RegisterCommand(
    string Name,
    string LastName,
    string Email,
    string Password) : IRequest<Result<UserResponse>>
{
}