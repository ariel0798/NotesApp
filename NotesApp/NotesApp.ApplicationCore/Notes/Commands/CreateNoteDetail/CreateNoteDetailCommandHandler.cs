using AutoMapper;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Note.Responses;
using NotesApp.ApplicationCore.Extensions;
using NotesApp.Domain.Errors.Exceptions;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.Commands.CreateNoteDetail;

public class CreateNoteDetailCommandHandler : NoteBase,  IRequestHandler<CreateNoteDetailCommand,Result<GetNoteDetailResponse>>
{
    private readonly INoteRepository _noteRepository;
    private readonly IMapper _mapper;

    public CreateNoteDetailCommandHandler(INoteRepository noteRepository, ISender mediator, IMapper mapper)
    : base(mediator)
    {
        _noteRepository = noteRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetNoteDetailResponse>> Handle(CreateNoteDetailCommand request, CancellationToken cancellationToken)
    {
        var noteId = await GetNoteId(cancellationToken);

        if (noteId == null)
            return new Result<GetNoteDetailResponse>().CreateException<GetNoteDetailResponse, NoteNotFoundException>();

        var note = await _noteRepository.GetById(noteId);
        note.NoteDetails ??= new List<NoteDetail>();
        
        var noteDetail = _mapper.Map<NoteDetail>(request);
        
        noteDetail.NoteDetailId =  Guid.NewGuid().ToString();
        note.NoteDetails.Add(noteDetail);
        await _noteRepository.Update(note);
        
        var noteDetailDto = _mapper.Map<GetNoteDetailResponse>(noteDetail);
        
        return noteDetailDto;
    }



}