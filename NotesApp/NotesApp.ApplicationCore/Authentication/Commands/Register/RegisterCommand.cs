using LanguageExt.Common;
using MediatR;

namespace NotesApp.ApplicationCore.Authentication.Commands.Register;

public record RegisterCommand(
    string Name,
    string LastName,
    string Email,
    string Password) : IRequest<Result<bool>>
{
    
}