using NotesApp.Api.Common;
using NotesApp.Api.Endpoints.Auth;
using NotesApp.Api.Extensions;

namespace NotesApp.Api.Summaries.Auth;

public class RefreshTokenEndpointSummary : ISummary<RefreshTokenEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
            .Summary("Generate refresh token", "Generate refresh token")
            .WithTags(ApiConstants.Authentication.Tag);
    }
}