using NotesApp.Api.Common;
using NotesApp.Api.Endpoints.Notes.Puts;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Contracts.ErrorResponses;

namespace NotesApp.Api.Summaries.Notes.Puts;

public class RecoverNoteDetailEndpointSummary : ISummary<RecoverNoteDetailEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
            .Response<bool>(StatusCodes.Status200OK, "Note recovered")
            .Response<ProblemResponse>(StatusCodes.Status404NotFound, "NoteDetailId not found")
            .Summary("Recovers a single note as defined by noteDetailId provided", "Recovers a single note as defined by noteDetailId provided")
            .WithTags(ApiConstants.Notes.Tag);
    }
}