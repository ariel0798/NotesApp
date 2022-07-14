using LanguageExt.Common;
using MediatR;

namespace NotesApp.ApplicationCore.Notes.Commands.DeleteNoteDetail;

public record DeleteNoteDetailCommand(string NoteDetailId) : IRequest<Result<bool>>
{
}