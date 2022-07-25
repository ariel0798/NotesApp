using FluentValidation;
using NotesApp.Domain.Errors.Messages;

namespace NotesApp.ApplicationCore.Notes.Commands.CreateNoteDetail;

public class CreateNoteDetailCommandValidation : AbstractValidator<CreateNoteDetailCommand>
{
    public CreateNoteDetailCommandValidation()
    {
        RuleFor(n => n.Title)
            .NotEmpty().WithMessage(ErrorMessages.Note.EmptyTitle)
            .NotNull().WithMessage(ErrorMessages.Note.NotNullTitle);

        RuleFor(n => n.Description)
            .NotEmpty().WithMessage(ErrorMessages.Note.EmptyDescription)
            .NotNull().WithMessage(ErrorMessages.Note.NotNullDescription);
    }
}