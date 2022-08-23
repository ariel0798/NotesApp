using MediatR;
using NotesApp.Api.Common;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Authentication.Commands.RefreshToken;
using NotesApp.ApplicationCore.Authentication.Models;

namespace NotesApp.Api.Endpoints.Auth;

public class RefreshTokenEndpoint : IEndpoint
{
    public static void DefineEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiConstants.Authentication.EndpointNames.RefreshToken,
            async (ISender mediator, HttpContext context ,CancellationToken ct) =>
            {
                var request = context?.Request;
                var refreshToken = request.Cookies["refreshToken"];
        
                var command = new RefreshTokenCommand(refreshToken);
        
                var result = await mediator.Send(command, ct);
        
                if(result.IsSuccess)
                {
                    var token = result.Match<JwtToken>(obj => obj, null);
                    LoginEndpoint.SetRefreshToken(token,context);
                }

                var resultToken = result.Map<string>(obj => obj.Token);
                return resultToken.ToOk();
            }
            ).FindSummary<RefreshTokenEndpoint>();
    }
}