namespace NotesApp.Domain.Errors.Exceptions;

public class EmailDuplicatedException: Exception
{
    public EmailDuplicatedException()
    {
    }

    public EmailDuplicatedException(string message)
        : base(message)
    {
    }

    public EmailDuplicatedException(string message, Exception inner)
        : base(message, inner)
    {
    }

}