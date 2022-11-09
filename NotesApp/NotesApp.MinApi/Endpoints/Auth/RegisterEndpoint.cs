using AutoMapper;
using MediatR;
using NotesApp.ApplicationCore.Authentication.Commands.Register;
using NotesApp.ApplicationCore.Contracts.Authentication.Requests;
using NotesApp.MinApi.Extensions;
using NotesApp.MinApi.Summaries.Auth;

namespace NotesApp.MinApi.Endpoints.Auth;

public class RegisterEndpoint : IEndpoint
{
    //TODO Return User, Use created url with ID and Change the Documentation from bool to the object
    //TODO Add all endpoints
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiConstants.Authentication.EndpointNames.Register, 
            async (RegisterUserRequest registerUserRequest,IMapper mapper, ISender mediator, CancellationToken ct) => 
        {
            var command = mapper.Map<RegisterCommand>(registerUserRequest);

            var result = await mediator.Send(command, ct);
            
            return result.ToOk();
        })
            .FindSummary<RegisterEndpoint>();
    }
}