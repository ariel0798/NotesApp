using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Endpoints.Auth;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Contracts.Authentication.Requests;

namespace NotesApp.Api.Summaries.Auth;

public class LoginEndpointSummary : ISummary<LoginEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
            .Accepts<LoginRequest>(ApiConstants.ContentType)
            .Response<string>(StatusCodes.Status200OK,"Jwt generated")
            .Response<ProblemDetails>(StatusCodes.Status409Conflict, "Authentication failed")
            .Summary("Login user", "Login user returns Jwt")
            .WithTags(ApiConstants.Authentication.Tag);
    }
}