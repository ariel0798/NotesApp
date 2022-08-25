using MediatR;
using Microsoft.AspNetCore.Authorization;
using NotesApp.Api.Common;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Notes.Commands.SoftDeleteNoteDetail;

namespace NotesApp.Api.Endpoints.Notes.Deletes;

public class SoftDeleteNoteDetailEndpoint : IEndpoint
{
    public static void DefineEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiConstants.Notes.EndpointNames.SoftDeleteByNoteDetailId,
            [Authorize] 
            async (string noteDetailId,ISender mediator,CancellationToken ct) =>
            {
                var result = await mediator.Send(new SoftDeleteNoteDetailCommand(noteDetailId), ct);

                return result.ToOk();
            })
            .FindSummary<SoftDeleteNoteDetailEndpoint>();
    }
}