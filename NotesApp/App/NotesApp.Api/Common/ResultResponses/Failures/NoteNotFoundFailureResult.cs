using System.Net;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Domain.Errors.Exceptions;

namespace NotesApp.Api.Common.ResultResponses.Failures;

public class NoteNotFoundFailureResult : IFailureResult
{
    public IResult GetFailureResult(Exception e)
    {
        var problemDetail = new ProblemDetails()
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            Title = e.Message,
            Status = (int)HttpStatusCode.NotFound,
        };
        return Results.NotFound(problemDetail);
    }

    public Type ExceptionType { get; } = typeof(NoteNotFoundException);
}