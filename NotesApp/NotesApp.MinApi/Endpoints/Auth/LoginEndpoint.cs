using AutoMapper;
using MediatR;
using NotesApp.ApplicationCore.Authentication.Commands.Login;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.ApplicationCore.Contracts.Authentication.Requests;
using NotesApp.MinApi.Extensions;
using NotesApp.MinApi.Summaries.Auth;

namespace NotesApp.MinApi.Endpoints.Auth;

public class LoginEndpoint : IEndpoint
{
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiConstants.Authentication.EndpointNames.Login, 
                async (LoginRequest loginRequest, IMapper mapper, ISender mediator, CancellationToken ct,
                    HttpContext context) => 
                {
                    var command = mapper.Map<LoginCommand>(loginRequest);
        
                    var result = await mediator.Send(command, ct);

                    if (result.IsSuccess)
                    {
                        var token = result.Match<JwtToken>(obj => obj, null);
                        SetRefreshToken(token,context);
                    }
        
                    var resultToken = result.Map<string>(obj => obj.Token);
                    return resultToken.ToOk();
                })
                    
            .FindSummary<LoginEndpoint>();
    }
    
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