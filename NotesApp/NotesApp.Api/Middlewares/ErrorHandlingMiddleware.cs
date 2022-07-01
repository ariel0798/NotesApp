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
        string response = JsonSerializer.Serialize(new ValidationProblemDetails()
        {
            Title = "An error  while processing your request.",
            Status = (int)HttpStatusCode.InternalServerError,
            Instance = context.Request.Path,
         
        });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(response);
    }
}