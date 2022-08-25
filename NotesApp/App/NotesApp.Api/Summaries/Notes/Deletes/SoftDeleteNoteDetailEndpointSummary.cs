using NotesApp.Api.Common;
using NotesApp.Api.Endpoints.Notes.Deletes;
using NotesApp.Api.Extensions;

namespace NotesApp.Api.Summaries.Notes.Deletes;

public class SoftDeleteNoteDetailEndpointSummary : ISummary<SoftDeleteNoteDetailEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
            .Response<bool>(StatusCodes.Status200OK, "Note soft deleted")
            .Summary("Soft delete note by the id provided", "Soft delete note by the id provided")
            .WithTags(ApiConstants.Notes.Tag);
    }
}