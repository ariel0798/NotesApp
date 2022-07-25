using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace NotesApp.Api.Extensions;

public static class ValidationExtensions
{
    public static ValidationProblemDetails ToProblemDetails(
        this ValidationException ex)
    {
        var error = new ValidationProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Status = 400
        };

        var errorDictionary = ex.Errors.GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray()
            );
        
        foreach (var errorPair in errorDictionary)
        {
            error.Errors.Add(errorPair);
        }

        return error;
    }
}