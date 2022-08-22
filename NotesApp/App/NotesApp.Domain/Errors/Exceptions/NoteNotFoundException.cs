using NotesApp.Domain.Errors.Messages;

namespace NotesApp.Domain.Errors.Exceptions;

public class NoteNotFoundException : Exception
{
    public NoteNotFoundException(string message = ErrorMessages.Note.NoteNotFound)
        : base(message)
    {
    }

}