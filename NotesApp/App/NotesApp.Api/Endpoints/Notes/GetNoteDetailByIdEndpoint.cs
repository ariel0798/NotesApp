using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using NotesApp.Api.Common;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Notes.Queries.GetNoteDetailById;

namespace NotesApp.Api.Endpoints.Notes;

public class GetNoteDetailByIdEndpoint : IEndpoint
{
    public static void DefineEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiConstants.Notes.EndpointNames.NoteDetailId,
            [Authorize] async (string noteDetailId,ISender mediator,  CancellationToken ct) =>
            {
                var result = await mediator.Send(new GetNoteDetailByIdQuery(noteDetailId), ct);
                return result.ToOk();
            })
            .FindSummary<GetNoteDetailByIdEndpoint>();
    }
}