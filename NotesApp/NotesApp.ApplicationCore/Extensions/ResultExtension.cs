using System.Security.Authentication;
using FluentValidation;
using FluentValidation.Results;
using LanguageExt.Common;
using NotesApp.Domain.Errors.Exceptions;
using NotesApp.Domain.Errors.Messages;

namespace NotesApp.ApplicationCore.Extensions;

public static class ExceptionExtension 
{
    public static Result<TResult> CreateException<TResult,TException>(
        this Result<TResult> result) where TException : Exception
    {
        if (typeof(TException) == typeof(EmailDuplicatedException))
        {
            return new Result<TResult>(new EmailDuplicatedException(ErrorMessages.User.DuplicatedEmail));
        }
        if (typeof(TException) == typeof(InvalidCredentialException))
        {
            return new Result<TResult>(new InvalidCredentialException(ErrorMessages.Authentication.InvalidCredentials));
        }
        

        return new Result<TResult>();
    }
    
    public static Result<TResult> CreateValidationException<TResult>(
        this Result<TResult> result, List<ValidationFailure> errors)
    {
        return new Result<TResult>(new ValidationException(errors));
    }
}