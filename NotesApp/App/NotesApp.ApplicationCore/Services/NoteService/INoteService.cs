using LanguageExt.Common;

namespace NotesApp.ApplicationCore.Services.NoteService;

public interface INoteService
{
    Result<T>? ValidateUserIdAndNoteDetailId<T>(int? userId, string noteDetailId);
}