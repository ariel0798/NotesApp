using FluentValidation;

namespace NotesApp.MinApi.Extensions.ResultResponses.Failures;

public class ValidationFailureResult : IFailureResult
{
    public IResult GetFailureResult(Exception e)
    {
        var validationException = (ValidationException)e;
        return Results.BadRequest(validationException.ToProblemDetails());
    }

    public Type ExceptionType { get; } = typeof(ValidationException);
}