using MediatR;
using NotesApp.ApplicationCore.Authentication.Commands.RefreshToken;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.MinApi.Extensions;

namespace NotesApp.MinApi.Endpoints.Auth;

public class RefreshTokenEndpoint : IEndpoint
{
    public static void DefineEndpoints(IEndpointRouteBuilder app)
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
                    SetRefreshToken(token,context);
                }

                var resultToken = result.Map<string>(obj => obj.Token);
                return resultToken.ToOk();
            });
    }
    //TODO make one method Static here and use it in Login 
    private static void SetRefreshToken(JwtToken refreshToken, HttpContext context)
    {
        var cookieOption = new CookieOptions()
        {
            HttpOnly = true,
            Expires = refreshToken.Expires
        };
        
        context.Response.Cookies.Append("refreshToken", refreshToken.RefreshToken, cookieOption);
    }
}