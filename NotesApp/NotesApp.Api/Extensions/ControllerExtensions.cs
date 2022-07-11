using System.Net;
using System.Security.Authentication;
using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Domain.Errors.Exceptions;

namespace NotesApp.Api.Extensions;

public static class ControllerExtensions
{
    public static IActionResult ToOk<TResult>(
        this Result<TResult> result)
    {
        return result.Match<IActionResult>(obj =>
        {
            return new OkObjectResult(obj);
        }, exception =>
        {
            return exception switch
            {
                ValidationException validationException => new BadRequestObjectResult(validationException.ToProblemDetails()),
                
                InvalidCredentialException invalidCredentialException => new ConflictObjectResult(
                    ProblemDetailsConflict(invalidCredentialException.Message)),
                
                EmailDuplicatedException emailDuplicatedException => new ConflictObjectResult(
                    ProblemDetailsConflict(emailDuplicatedException.Message)),
                
                NoteNotFoundException noteNotFoundException => new NotFoundObjectResult(
                    ProblemDetailsNotFound(noteNotFoundException.Message)),
                
                _ => new StatusCodeResult(500)
            };
        });
    }

    private static ProblemDetails ProblemDetailsConflict(string message)
    {
        return new ProblemDetails()
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            Title = message,
            Status = (int)HttpStatusCode.Conflict,
        };
    }
    
    private static ProblemDetails ProblemDetailsNotFound(string message)
    {
        return new ProblemDetails()
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            Title = message,
            Status = (int)HttpStatusCode.NotFound,
        };
    }
}