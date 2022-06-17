using MediatR;
using NotesApp.ApplicationCore.Notes.Commands;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.CommandHandlers;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Note>
{
    private readonly INoteRepository _noteRepository;

    public CreateNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    public async Task<Note> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new Note();

        await _noteRepository.Create(note);
        
        return note;
    }
}