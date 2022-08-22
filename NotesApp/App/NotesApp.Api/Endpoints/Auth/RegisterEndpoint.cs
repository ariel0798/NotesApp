using AutoMapper;
using MediatR;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Authentication.Commands.Register;
using NotesApp.ApplicationCore.Contracts.Authentication.Requests;

namespace NotesApp.Api.Endpoints.Auth;

public class RegisterEndpoint : IEndpoint
{
    public static void DefineEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiConstants.Authentication.EndpointNames.Register, async (RegisterRequest registerUserRequest,
                IMapper mapper, ISender mediator, CancellationToken ct) =>
            {
                var command = mapper.Map<RegisterCommand>(registerUserRequest);

                var result = await mediator.Send(command, ct);
            
                return result.ToOk();
            }
        );
    }
}