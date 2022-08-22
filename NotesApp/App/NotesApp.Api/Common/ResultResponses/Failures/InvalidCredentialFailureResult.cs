using System.Net;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace NotesApp.Api.Common.ResultResponses.Failures;

public class InvalidCredentialFailureResult : IFailureResult
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

    public Type ExceptionType { get; } = typeof(InvalidCredentialException);
}