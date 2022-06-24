using MediatR;
using NotesApp.ApplicationCore.Notes.Commands;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.CommandHandlers;

public class DeleteNoteDetailCommandHandler : IRequestHandler<DeleteNoteDetailCommand,NoteDetail>
{
    private readonly INoteRepository _noteRepository;

    public DeleteNoteDetailCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    public async Task<NoteDetail> Handle(DeleteNoteDetailCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetById(request.NoteId);

        var noteDetail = note.NoteDetails.FirstOrDefault(n => n.NoteDetailId.ToString() == request.NoteDetailId);

        if (noteDetail == null)
            return noteDetail;

        note.NoteDetails.Remove(noteDetail);

        await _noteRepository.Update(note);
        
        return noteDetail;
    }
}