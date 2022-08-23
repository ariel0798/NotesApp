using System.Security.Authentication;
using NotesApp.Domain.Errors.Messages;

namespace NotesApp.Domain.Errors.Exceptions.Factory;

public static class ExceptionFactory
{
    public static EmailDuplicatedException EmailDuplicatedException =>
        new EmailDuplicatedException(ErrorMessages.User.DuplicatedEmail);

    public static NoteNotFoundException NoteNotFoundException =>
        new NoteNotFoundException(ErrorMessages.Note.NoteNotFound);

    public static InvalidCredentialException InvalidCredentialException =>
        new InvalidCredentialException(ErrorMessages.Authentication.InvalidCredentials);
}