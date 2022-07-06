using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace NotesApp.Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error was ocurred during the process. Path: {context.Request.Path}. Message {ex.Message}");

            await HandleExceptionMessageAsync(context).ConfigureAwait(false);
        }
    }

    private static Task HandleExceptionMessageAsync(HttpContext context)
    {
        var problemDetails = new ProblemDetails()
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Title = "An error  while processing your request.",
            Status = (int)HttpStatusCode.InternalServerError,
        };
        
        var traceId = Activity.Current?.Id ?? context?.TraceIdentifier;
        if (traceId != null)
            problemDetails.Extensions["traceId"] = traceId;
        
        string response = JsonSerializer.Serialize(problemDetails);
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(response);
    }
}