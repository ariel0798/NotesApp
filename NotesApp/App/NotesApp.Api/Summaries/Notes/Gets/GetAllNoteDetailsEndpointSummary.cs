using NotesApp.Api.Common;
using NotesApp.Api.Endpoints.Notes.Gets;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;

namespace NotesApp.Api.Summaries.Notes.Gets;

public class GetAllNoteDetailsEndpointSummary : ISummary<GetAllNoteDetailsEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
            .Response<IEnumerable<NoteDetailResponse>>(StatusCodes.Status200OK, "Returns all notes")
            .Summary("Returns all notes of a User", "Returns all notes of a User")
            .WithTags(ApiConstants.Notes.Tag);
    }
}