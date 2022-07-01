using FluentValidation;
using MediatR;
using NotesApp.ApplicationCore.Contracts.User.Requests;
using NotesApp.ApplicationCore.Users.Queries;

namespace NotesApp.ApplicationCore.Validation.Requests.Users;

public class RegisterUserRequestValidation : AbstractValidator<RegisterUserRequest>
{
    private readonly IMediator _mediator;

    public RegisterUserRequestValidation(IMediator mediator)
    {
        _mediator = mediator;

        RuleFor(c => c.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .MustAsync(IsNotExistingUserEmail).WithMessage("Email already exists");
        
        RuleFor(c => c.Password)
            .NotEmpty()
            .NotNull()
            .Length(7, 150);
        
        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull();
        
        RuleFor(c => c.LastName)
            .NotEmpty()
            .NotNull();
    }

    private async Task<bool> IsNotExistingUserEmail(string email,CancellationToken cancellationToken)
    {
        if (email == null || email == string.Empty)
            return false;
        
        var query = new GetUserByEmailQuery() { Email = email };

        var user = await _mediator.Send(query,cancellationToken);

        var isNotExistingUser = user == null;
        return isNotExistingUser;
    }
}