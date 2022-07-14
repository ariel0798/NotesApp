using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Extensions;
using NotesApp.Domain.Errors.Exceptions;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.Commands.DeleteNoteDetail;

public class DeleteNoteDetailCommandHandler : NoteBase, IRequestHandler<DeleteNoteDetailCommand,Result<bool>>
{
    private readonly INoteRepository _noteRepository;
    
    public DeleteNoteDetailCommandHandler(ISender mediator, INoteRepository noteRepository) 
        : base(mediator)
    {
        _noteRepository = noteRepository;
    }

    public async Task<Result<bool>> Handle(DeleteNoteDetailCommand request, CancellationToken cancellationToken)
    {
        var noteId = await GetNoteId(cancellationToken);
        
        if (noteId == null)
            return new Result<bool>().CreateException<bool,NoteNotFoundException>();

        var noteDetail = await DeleteNoteDetail(noteId, request.NoteDetailId);

        return noteDetail != null ? true : new Result<bool>().CreateException<bool,NoteNotFoundException>(); ;
    }

    private async Task<NoteDetail> DeleteNoteDetail(string noteId, string noteDetailId)
    {
        var note = await _noteRepository.GetById(noteId);

        var noteDetail = note.NoteDetails.FirstOrDefault(n => n.NoteDetailId.ToString() == noteDetailId);

        if (noteDetail == null)
            return noteDetail;

        note.NoteDetails.Remove(noteDetail);

        await _noteRepository.Update(note);
        
        return noteDetail;
    }
}