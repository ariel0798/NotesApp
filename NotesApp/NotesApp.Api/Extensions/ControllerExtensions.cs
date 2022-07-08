using System.Net;
using System.Security.Authentication;
using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

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
            if (exception is ValidationException validationException)
            {
                return new BadRequestObjectResult(validationException.ToProblemDetails());
            }

            if (exception is InvalidCredentialException invalidCredentialException)
            {
                return new ConflictObjectResult(ProblemDetailsConflict(invalidCredentialException.Message));
            }

            return new StatusCodeResult(500);
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
}