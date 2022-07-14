using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Note.Responses;

namespace NotesApp.ApplicationCore.Notes.Commands.UpdateNoteDetail;

public record UpdateNoteDetailCommand(
    string NoteDetailId,
    string Title,
    string Description) : IRequest<Result<GetNoteDetailResponse>>
{
}