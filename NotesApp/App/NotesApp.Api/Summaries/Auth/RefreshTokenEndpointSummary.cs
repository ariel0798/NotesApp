using NotesApp.Api.Common;
using NotesApp.Api.Endpoints.Auth;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Contracts.ErrorResponses;

namespace NotesApp.Api.Summaries.Auth;

public class RefreshTokenEndpointSummary : ISummary<RefreshTokenEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
            .Summary("Generate refresh token", "Generate refresh token")
            .Response<string>(StatusCodes.Status200OK,"Returns new refresh token")
            .Response<ProblemResponse>(StatusCodes.Status409Conflict,"Invalid refresh Token")
            .WithTags(ApiConstants.Authentication.Tag);
    }
}