namespace NotesApp.Domain.Errors.Exceptions;

public class NoteNotFoundException : Exception
{
    public NoteNotFoundException()
    {
    }

    public NoteNotFoundException(string message)
        : base(message)
    {
    }

    public NoteNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
    
}