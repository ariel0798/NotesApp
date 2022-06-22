using MediatR;
using NotesApp.ApplicationCore.Notes.Commands;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.CommandHandlers;

public class SoftDeleteNoteDetailCommandHandler : IRequestHandler<SoftDeleteNoteDetailCommand,NoteDetail>
{
    private readonly INoteRepository _noteRepository;
    
    public SoftDeleteNoteDetailCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    public async Task<NoteDetail> Handle(SoftDeleteNoteDetailCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetById(request.NoteId);

        var noteDetail = note.NoteDetails.FirstOrDefault(n => n.NoteDetailId.ToString() == request.NoteDetailId);

        if (noteDetail == null)
            return noteDetail;

        noteDetail.IsDeleted = true;
        noteDetail.NoteDeleted = DateTime.Now;

        await _noteRepository.Update(note);
        
        return noteDetail;
    }
}