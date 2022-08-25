using MediatR;
using Microsoft.AspNetCore.Authorization;
using NotesApp.Api.Common;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Notes.Queries.GetAllNoteDetailsTrash;

namespace NotesApp.Api.Endpoints.Notes;

public class GetAllNoteDetailsTrashEndpoint : IEndpoint
{
    public static void DefineEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiConstants.Notes.EndpointNames.Trash,
                [Authorize] async (ISender mediator, CancellationToken ct) =>
                {
                    var result = await mediator.Send(new GetAllNoteDetailsTrashQuery(), ct);

                    return result.ToOk();
                })
            .FindSummary<GetAllNoteDetailsTrashEndpoint>();
    }
}