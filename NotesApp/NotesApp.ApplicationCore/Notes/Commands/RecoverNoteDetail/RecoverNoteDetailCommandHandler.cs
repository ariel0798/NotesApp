using AutoMapper;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Extensions;
using NotesApp.Domain.Errors.Exceptions;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.Commands.RecoverNoteDetail;

public class RecoverNoteDetailCommandHandler : NoteBase, IRequestHandler<RecoverNoteDetailCommand,Result<bool>>
{
    private readonly INoteRepository _noteRepository;
    private readonly IMapper _mapper;
    
    public RecoverNoteDetailCommandHandler(ISender mediator, INoteRepository noteRepository, IMapper mapper) 
        : base(mediator)
    {
        _noteRepository = noteRepository;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(RecoverNoteDetailCommand request, CancellationToken cancellationToken)
    {
        var noteId = await GetNoteId(cancellationToken);
        
        if (noteId == null)
            return new Result<bool>().CreateException<bool,NoteNotFoundException>();

        var noteDetail = await RecoverNoteDetail(noteId, request.NoteDetailId);
        
        if (noteDetail == null)
            return new Result<bool>().CreateException<bool,NoteNotFoundException>();

        return !noteDetail.IsDeleted;
    }

    private async Task<NoteDetail?> RecoverNoteDetail(string noteId, string noteDetailId)
    {
        var note = await _noteRepository.GetById(noteId);

        var noteDetail = note.NoteDetails.FirstOrDefault(n => n.NoteDetailId.ToString() == noteDetailId);

        if (noteDetail == null)
            return noteDetail;

        noteDetail.IsDeleted = false;

        await _noteRepository.Update(note);
        
        return noteDetail;
    }
}