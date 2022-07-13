using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Authentication.Models;

namespace NotesApp.ApplicationCore.Authentication.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken): IRequest<Result<JwtToken>>
{
}