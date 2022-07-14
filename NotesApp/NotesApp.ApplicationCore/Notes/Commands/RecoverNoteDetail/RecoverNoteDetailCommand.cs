using LanguageExt.Common;
using MediatR;

namespace NotesApp.ApplicationCore.Notes.Commands.RecoverNoteDetail;

public record RecoverNoteDetailCommand(string NoteDetailId) : IRequest<Result<bool>>
{
    
}