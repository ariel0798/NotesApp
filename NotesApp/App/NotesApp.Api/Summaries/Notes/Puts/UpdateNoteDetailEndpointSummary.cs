using NotesApp.Api.Common;
using NotesApp.Api.Endpoints.Notes.Puts;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Contracts.ErrorResponses;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;
using NotesApp.ApplicationCore.Contracts.User.Requests;

namespace NotesApp.Api.Summaries.Notes.Puts;

public class UpdateNoteDetailEndpointSummary : ISummary<UpdateNoteDetailEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
            .Accepts<UpdateNoteDetailRequest>(ApiConstants.ContentType)
            .Response<NoteDetailResponse>(StatusCodes.Status200OK, "Note updated")
            .Response<ValidationResponse>(StatusCodes.Status400BadRequest, "Validation failed")
            .Summary("Updates note by id provided", "Updates note by id provided")
            .WithTags(ApiConstants.Notes.Tag);
    }
}