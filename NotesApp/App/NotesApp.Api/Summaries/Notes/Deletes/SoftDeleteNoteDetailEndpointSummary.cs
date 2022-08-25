using NotesApp.Api.Common;
using NotesApp.Api.Endpoints.Notes.Deletes;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Contracts.ErrorResponses;

namespace NotesApp.Api.Summaries.Notes.Deletes;

public class SoftDeleteNoteDetailEndpointSummary : ISummary<SoftDeleteNoteDetailEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
            .Response<bool>(StatusCodes.Status200OK, "Note soft deleted")
            .Response<ProblemResponse>(StatusCodes.Status404NotFound, "NoteDetailId not found")
            .Summary("Soft deletes a single note as defined by the id provided", "Soft deletes a single note as defined by the id provided")
            .WithTags(ApiConstants.Notes.Tag);
    }
}