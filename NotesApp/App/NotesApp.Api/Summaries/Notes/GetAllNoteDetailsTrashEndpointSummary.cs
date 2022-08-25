using NotesApp.Api.Common;
using NotesApp.Api.Endpoints.Notes;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;

namespace NotesApp.Api.Summaries.Notes;

public class GetAllNoteDetailsTrashEndpointSummary : ISummary<GetAllNoteDetailsTrashEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
            .Response<IEnumerable<NoteDetailResponse>>(StatusCodes.Status200OK, "Returns all notes in trash")
            .Summary("Get All Note detail in trash of user", "Get All Note detail in trash of user")
            .WithTags(ApiConstants.Notes.Tag);
    }
}