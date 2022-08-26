using AutoMapper;
using MediatR;
using NotesApp.Api.Common;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Authentication.Commands.Login;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.ApplicationCore.Contracts.Authentication.Requests;
using NotesApp.ApplicationCore.Services.AuthService;

namespace NotesApp.Api.Endpoints.Auth;

public class LoginEndpoint : IEndpoint
{
    public static void DefineEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiConstants.Authentication.EndpointNames.Login,
            async (LoginRequest loginRequest, IMapper mapper, ISender mediator, IAuthService authService, CancellationToken ct,
                HttpContext context) =>
            {
                var command = mapper.Map<LoginCommand>(loginRequest);

                var result = await mediator.Send(command, ct);

                if (result.IsSuccess)
                {
                    var token = result.Match<JwtToken>(obj => obj, null);
                    authService.SetRefreshToken(token, context);
                }

                var resultToken = result.Map<string>(obj => obj.Token);
                return resultToken.ToOk();
            }
            ).FindSummary<LoginEndpoint>();
    }
    
}