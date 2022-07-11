using FluentValidation;
using NotesApp.ApplicationCore.Contracts.Note.Requests;
using NotesApp.Domain.Errors.Messages;

namespace NotesApp.ApplicationCore.Validation.Requests.Notes;

public class UpdateNoteDetailRequestValidation : AbstractValidator<UpdateNoteDetailRequest>
{
    public UpdateNoteDetailRequestValidation()
    {
        RuleFor(n => n.NoteDetailId)
            .NotNull();
        
        RuleFor(n => n.Title)
            .NotEmpty().WithMessage(ErrorMessages.Note.EmptyTitle)
            .NotNull().WithMessage(ErrorMessages.Note.NotNullTitle);

        RuleFor(n => n.Description)
            .NotEmpty().WithMessage(ErrorMessages.Note.EmptyDescription)
            .NotNull().WithMessage(ErrorMessages.Note.NotNullDescription);
    }
}