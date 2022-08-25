using NotesApp.Api.Common;
using NotesApp.Api.Endpoints.Notes;
using NotesApp.Api.Extensions;

namespace NotesApp.Api.Summaries.Notes;

public class DeleteNoteDetailEndpointSummary : ISummary<DeleteNoteDetailEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
            .Produces(StatusCodes.Status204NoContent)
            .Summary("Deletes a single note as defined by the id provided",
                "Deletes a single note as defined by the id provided")
            .WithTags(ApiConstants.Notes.Tag);
    }
}