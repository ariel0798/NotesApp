using NotesApp.Api.Common;
using NotesApp.Api.Endpoints.Notes.Deletes;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Contracts.ErrorResponses;

namespace NotesApp.Api.Summaries.Notes.Deletes;

public class DeleteNoteDetailEndpointSummary : ISummary<DeleteNoteDetailEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
            .Produces(StatusCodes.Status204NoContent)
            .Response<ProblemResponse>(StatusCodes.Status404NotFound, "NoteDetailId not found")
            .Summary("Deletes a single note as defined by the id provided",
                "Deletes a single note as defined by the id provided")
            .WithTags(ApiConstants.Notes.Tag);
    }
}