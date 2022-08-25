using NotesApp.Api.Common;
using NotesApp.Api.Endpoints.Notes.Gets;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Contracts.ErrorResponses;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;

namespace NotesApp.Api.Summaries.Notes.Gets;

public class GetNoteDetailByIdEndpointSummary : ISummary<GetNoteDetailByIdEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
            .Response<NoteDetailResponse>(StatusCodes.Status200OK, "Returns note")
            .Response<ProblemResponse>(StatusCodes.Status404NotFound, "NoteDetailId not found")
            .Summary("Returns a single note as defined by noteDetailId provided","Returns a single note as defined by noteDetailId provided")
            .WithTags(ApiConstants.Notes.Tag);

    }
}