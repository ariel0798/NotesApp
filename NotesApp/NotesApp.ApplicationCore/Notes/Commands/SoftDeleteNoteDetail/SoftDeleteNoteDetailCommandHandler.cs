using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Extensions;
using NotesApp.Domain.Errors.Exceptions;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.Commands.SoftDeleteNoteDetail;

public class SoftDeleteNoteDetailCommandHandler : NoteBase, IRequestHandler<SoftDeleteNoteDetailCommand,Result<bool>>
{
    private readonly INoteRepository _noteRepository;
    
    public SoftDeleteNoteDetailCommandHandler(ISender mediator, INoteRepository noteRepository) 
        : base(mediator)
    {
        _noteRepository = noteRepository;
    }

    public async Task<Result<bool>> Handle(SoftDeleteNoteDetailCommand request, CancellationToken cancellationToken)
    {
        var noteId = await GetNoteId(cancellationToken);
        
        if (noteId == null)
            return new Result<bool>().CreateException<bool,NoteNotFoundException>();

        var noteDetail = await SoftDeleteNoteDetail(noteId, request.NoteDetailId);
        
        return noteDetail == null ? new Result<bool>().CreateException<bool,NoteNotFoundException>() : noteDetail.IsDeleted;
    }

    private async Task<NoteDetail?> SoftDeleteNoteDetail(string noteId, string noteDetailId)
    {
        var note = await _noteRepository.GetById(noteId);

        var noteDetail = note.NoteDetails.FirstOrDefault(n => n.NoteDetailId.ToString() == noteDetailId);

        if (noteDetail == null)
            return noteDetail;

        noteDetail.IsDeleted = true;
        noteDetail.NoteDeleted = DateTime.Now;

        await _noteRepository.Update(note);
        
        return noteDetail;
    }
}