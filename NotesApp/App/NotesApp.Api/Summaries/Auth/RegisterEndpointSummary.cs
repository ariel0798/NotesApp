using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Endpoints.Auth;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Contracts.Authentication.Requests;

namespace NotesApp.Api.Summaries.Auth;

public class RegisterEndpointSummary : ISummary<RegisterEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
                .Accepts<RegisterRequest>(ApiConstants.ContentType)
                .Response<bool>(StatusCodes.Status201Created,"User created")
                .Response<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "Validation failed")
                .Summary("Register user", "Register user")
                .WithTags(ApiConstants.Authentication.Tag);
    }
}

