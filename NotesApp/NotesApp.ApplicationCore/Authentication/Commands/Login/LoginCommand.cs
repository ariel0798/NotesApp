using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Authentication.Models;

namespace NotesApp.ApplicationCore.Authentication.Commands.Login;

public record LoginCommand(
    string Email,
    string Password) :  IRequest<Result<JwtToken>>
{
}