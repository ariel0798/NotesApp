using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using NotesApp.Api.Common;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Contracts.Notes.Requests;
using NotesApp.ApplicationCore.Notes.Commands.CreateNoteDetail;

namespace NotesApp.Api.Endpoints.Notes;

public class CreateNoteDetailEndpoint : IEndpoint
{
    public static void DefineEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiConstants.Notes.BaseRoute, 
            [Authorize]
            async (CreateNoteDetailRequest createNoteDetailRequest, ISender mediator
                , IMapper mapper, CancellationToken ct) =>
        {
            var command = mapper.Map<CreateNoteDetailCommand>(createNoteDetailRequest);
            var result = await mediator.Send(command, ct);

            var url = ApiConstants.Notes.EndpointNames.NoteDetailId;

            if (result.IsSuccess)
                url = ApiConstants.Notes.BaseRoute + $"/{result.Map(x => x.NoteDetailId)}";
            
            return result.ToOk(StatusCodes.Status201Created, 
                url);
            
        }).FindSummary<CreateNoteDetailEndpoint>();
    }
}