using NotesApp.Api.Common;
using NotesApp.Api.Endpoints.Notes;
using NotesApp.Api.Extensions;

namespace NotesApp.Api.Summaries.Notes;

public class GetNoteDetailByIdEndpointSummary : ISummary<GetNoteDetailByIdEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
            .Summary("Get Note detail by noteDetailId","Get Note detail by noteDetailId")
            .WithTags(ApiConstants.Notes.Tag);

    }
}