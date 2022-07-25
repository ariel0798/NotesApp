using FluentValidation;
using NotesApp.Domain.Errors.Messages;

namespace NotesApp.ApplicationCore.Notes.Commands.UpdateNoteDetail;

public class UpdateNoteDetailCommandValidation : AbstractValidator<UpdateNoteDetailCommand>
{
    public UpdateNoteDetailCommandValidation()
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