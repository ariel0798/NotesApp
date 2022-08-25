using MediatR;
using Microsoft.AspNetCore.Authorization;
using NotesApp.Api.Common;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Notes.Queries.GetAllNoteDetails;

namespace NotesApp.Api.Endpoints.Notes.Gets;

public class GetAllNoteDetailsEndpoint : IEndpoint
{
    public static void DefineEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiConstants.Notes.BaseRoute, 
            [Authorize]
            async (ISender mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new GetAllNoteDetailsQuery(), ct);
                
                return result.ToOk();
            })
            .FindSummary<GetAllNoteDetailsEndpoint>();
    }
}