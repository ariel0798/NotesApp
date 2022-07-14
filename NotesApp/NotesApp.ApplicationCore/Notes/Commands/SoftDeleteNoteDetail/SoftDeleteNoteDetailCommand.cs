using LanguageExt.Common;
using MediatR;

namespace NotesApp.ApplicationCore.Notes.Commands.SoftDeleteNoteDetail;

public record SoftDeleteNoteDetailCommand(string NoteDetailId) : IRequest<Result<bool>>
{
}