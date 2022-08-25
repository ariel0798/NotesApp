using NotesApp.Api.Common;
using NotesApp.Api.Endpoints.Notes;
using NotesApp.Api.Extensions;

namespace NotesApp.Api.Summaries.Notes;

public class RecoverNoteDetailEndpointSummary : ISummary<RecoverNoteDetailEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
            .Response<bool>(StatusCodes.Status200OK, "Note recovered")
            .Summary("Recovers note by the id provided", "Recovers note by the id provided")
            .WithTags(ApiConstants.Notes.Tag);
    }
}