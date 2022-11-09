using Microsoft.AspNetCore.Mvc;
using NotesApp.ApplicationCore.Contracts.Authentication.Requests;
using NotesApp.MinApi.Endpoints.Auth;
using NotesApp.MinApi.Extensions;


namespace NotesApp.MinApi.Summaries.Auth;

public class RegisterEndpointSummary : ISummary<RegisterEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
            .Accepts<RegisterUserRequest>(ApiConstants.ContentType)
            .Response<bool>(StatusCodes.Status201Created,"User created")
            .Response<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "Validation failed")
            .Summary("Register user", "Register user")
            .WithTags(ApiConstants.Authentication.Tag);
    }
}