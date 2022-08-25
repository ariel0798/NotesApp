using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using NotesApp.Api.Common;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Contracts.User.Requests;
using NotesApp.ApplicationCore.Notes.Commands.UpdateNoteDetail;

namespace NotesApp.Api.Endpoints.Notes;

public class UpdateNoteDetailEndpoint : IEndpoint
{
    public static void DefineEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiConstants.Notes.BaseRoute,
                [Authorize] 
                async (UpdateNoteDetailRequest updateNoteDetailRequest, ISender mediator, IMapper mapper, CancellationToken ct) =>
                {
                    var command = mapper.Map<UpdateNoteDetailCommand>(updateNoteDetailRequest);

                    var result =  await mediator.Send(command, ct);

                   return result.ToOk();
                })
            .FindSummary<UpdateNoteDetailEndpoint>();
    }
}