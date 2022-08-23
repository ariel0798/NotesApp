using NotesApp.Domain.Errors.Messages;

namespace NotesApp.Domain.Errors.Exceptions;

public class EmailDuplicatedException: Exception
{
    public EmailDuplicatedException(string message)
        : base(message)
    {
    }

}