using System.Net;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Domain.Errors.Exceptions;

namespace NotesApp.MinApi.Extensions.ResultResponses.Failures;

public class EmailDuplicatedFailureResult : IFailureResult
{
    public IResult GetFailureResult(Exception e)
    {
        var problemDetail = new ProblemDetails()
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            Title = e.Message,
            Status = (int)HttpStatusCode.Conflict,
        };
        return Results.Conflict(problemDetail);
    }

    public Type ExceptionType { get; } = typeof(EmailDuplicatedException);
}