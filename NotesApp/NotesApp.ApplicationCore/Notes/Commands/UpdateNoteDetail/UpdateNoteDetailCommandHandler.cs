using AutoMapper;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Note.Responses;
using NotesApp.ApplicationCore.Extensions;
using NotesApp.Domain.Errors.Exceptions;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.Commands.UpdateNoteDetail;

public class UpdateNoteDetailCommandHandler : NoteBase, IRequestHandler<UpdateNoteDetailCommand,Result<GetNoteDetailResponse>>
{

    private readonly INoteRepository _noteRepository;
    private readonly IMapper _mapper;
    
    public UpdateNoteDetailCommandHandler(ISender mediator, INoteRepository noteRepository, IMapper mapper) 
        : base(mediator)
    {
        _noteRepository = noteRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetNoteDetailResponse>> Handle(UpdateNoteDetailCommand request, CancellationToken cancellationToken)
    {
        var noteId = await GetNoteId(cancellationToken);
        
        if (noteId == null)
            return new Result<GetNoteDetailResponse>().CreateException<GetNoteDetailResponse,NoteNotFoundException>();


        var noteDetail = await UpdateNoteDetail(request, noteId);
        
        if (noteDetail == null)
            return new Result<GetNoteDetailResponse>().CreateException<GetNoteDetailResponse,NoteNotFoundException>();

        var noteDetailGetDto = _mapper.Map<GetNoteDetailResponse>(noteDetail);
        
        return noteDetailGetDto;
    }

    private async Task<NoteDetail?> UpdateNoteDetail(UpdateNoteDetailCommand request, string noteId)
    {
        var note = await _noteRepository.GetById(noteId);

        var noteDetail = note.NoteDetails.FirstOrDefault(n => n.NoteDetailId == request.NoteDetailId);

        if (noteDetail == null)
            return noteDetail;
        
        noteDetail.Title = request.Title;
        noteDetail.Description = request.Description;

        await _noteRepository.Update(note);
        return noteDetail;
    }
}