using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Endpoints.Auth;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Contracts.Authentication.Requests;
using NotesApp.ApplicationCore.Contracts.ErrorResponses;
using NotesApp.ApplicationCore.Contracts.User.Responses;

namespace NotesApp.Api.Summaries.Auth;

public class RegisterEndpointSummary : ISummary<RegisterEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
                .Accepts<RegisterRequest>(ApiConstants.ContentType)
                .Response<UserResponse>(StatusCodes.Status201Created,"User created")
                .Response<ValidationResponse>(StatusCodes.Status400BadRequest, "Validation failed")
                .Summary("Register user", "Register user")
                .WithTags(ApiConstants.Authentication.Tag);
    }
}

