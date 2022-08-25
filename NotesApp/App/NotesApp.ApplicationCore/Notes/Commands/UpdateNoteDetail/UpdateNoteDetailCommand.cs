using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;

namespace NotesApp.ApplicationCore.Notes.Commands.UpdateNoteDetail;

public record UpdateNoteDetailCommand(
    string NoteDetailId,
    string Title,
    string Description) : IRequest<Result<NoteDetailResponse>>
{
    
}