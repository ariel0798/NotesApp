using MediatR;
using Microsoft.AspNetCore.Authorization;
using NotesApp.Api.Common;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Notes.Commands.DeleteNoteDetail;

namespace NotesApp.Api.Endpoints.Notes.Deletes;

public class DeleteNoteDetailEndpoint : IEndpoint
{
    public static void DefineEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiConstants.Notes.EndpointNames.NoteDetailId,
            [Authorize] 
            async (string noteDetailId, ISender mediator,CancellationToken ct) =>
            {
                var result = await mediator.Send(new DeleteNoteDetailCommand(noteDetailId), ct);

                return result.ToOk(StatusCodes.Status204NoContent);
            })
            .FindSummary<DeleteNoteDetailEndpoint>();
    }
}