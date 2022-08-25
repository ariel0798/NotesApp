using MediatR;
using Microsoft.AspNetCore.Authorization;
using NotesApp.Api.Common;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Notes.Commands.RecoverNoteDetail;

namespace NotesApp.Api.Endpoints.Notes.Puts;

public class RecoverNoteDetailEndpoint : IEndpoint
{
    public static void DefineEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiConstants.Notes.EndpointNames.Recover,
                [Authorize]
                async (string noteDetailId, ISender mediator,CancellationToken ct) =>
                {
                    var result =  await mediator.Send(new RecoverNoteDetailCommand(noteDetailId), ct);

                    return result.ToOk();
                })
            .FindSummary<RecoverNoteDetailEndpoint>();
    }
}