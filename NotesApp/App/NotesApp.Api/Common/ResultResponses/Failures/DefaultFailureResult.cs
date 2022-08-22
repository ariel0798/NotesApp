using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace NotesApp.Api.Common.ResultResponses.Failures;

public class DefaultFailureResult : IFailureResult
{
    public IResult GetFailureResult(Exception e)
    {
        var problemDetail = new ProblemDetails()
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Title = "An error  while processing your request.",
            Status = (int)HttpStatusCode.InternalServerError,
        };
        
        return Results.Problem(problemDetail);
    }

    public Type ExceptionType { get; } = typeof(Type);
}