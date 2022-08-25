using NotesApp.Api.Common;
using NotesApp.Api.Endpoints.Notes.Posts;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Contracts.ErrorResponses;
using NotesApp.ApplicationCore.Contracts.Notes.Requests;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;

namespace NotesApp.Api.Summaries.Notes.Posts;

public class CreateNoteDetailEndpointSummary : ISummary<CreateNoteDetailEndpoint>
{
    public static RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder)
    {
        return builder
            .Accepts<CreateNoteDetailRequest>(ApiConstants.ContentType)
            .Response<NoteDetailResponse>(StatusCodes.Status201Created, "Note created")
            .Response<ValidationResponse>(StatusCodes.Status400BadRequest, "Validation failed")
            .Summary("Add note", "Add note")
            .WithTags(ApiConstants.Notes.Tag);
    }
}