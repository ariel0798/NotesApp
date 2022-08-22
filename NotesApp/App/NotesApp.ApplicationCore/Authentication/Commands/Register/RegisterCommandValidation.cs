using FluentValidation;
using NotesApp.Domain.Errors.Messages;

namespace NotesApp.ApplicationCore.Authentication.Commands.Register;

public class RegisterCommandValidation : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidation()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage(ErrorMessages.User.EmptyEmail)
            .NotNull().WithMessage(ErrorMessages.User.NotNullEmail)
            .EmailAddress().WithMessage(ErrorMessages.User.InvalidEmailFormat);

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage(ErrorMessages.User.EmptyPassword)
            .NotNull().WithMessage(ErrorMessages.User.NotNullPassword)
            .Length(7, 150);

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage(ErrorMessages.User.EmptyName)
            .NotNull().WithMessage(ErrorMessages.User.NotNullName);
        
        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage(ErrorMessages.User.EmptyLastName)
            .NotNull().WithMessage(ErrorMessages.User.NotNullLastName);
    }

}
