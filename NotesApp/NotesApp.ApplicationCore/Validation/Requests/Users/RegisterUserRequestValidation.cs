using FluentValidation;
using NotesApp.ApplicationCore.Contracts.User.Requests;
using NotesApp.Domain.Errors.Messages;

namespace NotesApp.ApplicationCore.Validation.Requests.Users;

public class RegisterUserRequestValidation : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidation()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage(Errors.Messages.User.EmptyName)
            .NotNull()
            .EmailAddress().WithMessage(Errors.Messages.User.InvalidEmailFormat);

        RuleFor(c => c.Password)
            .NotEmpty()
            .NotNull()
            .Length(7, 150);

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage(Errors.Messages.User.EmptyName)
            .NotNull();
        
        RuleFor(c => c.LastName)
            .NotEmpty()
            .NotNull();
    }
}