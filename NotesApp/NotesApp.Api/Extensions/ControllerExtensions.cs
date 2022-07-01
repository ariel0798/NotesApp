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
            return new StatusCodeResult(500);
        });
    }
}