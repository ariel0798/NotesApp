using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace NotesApp.Api.Common.ResultResponses.Failures;

public class ValidationFailureResult : IFailureResult
{
    public IResult GetFailureResult(Exception e)
    {
        var validationException = (ValidationException)e;
        return Results.BadRequest(ToProblemDetails(validationException));
    }

    public Type ExceptionType { get; } = typeof(ValidationException);
    
    
    private static ValidationProblemDetails ToProblemDetails( ValidationException ex)
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