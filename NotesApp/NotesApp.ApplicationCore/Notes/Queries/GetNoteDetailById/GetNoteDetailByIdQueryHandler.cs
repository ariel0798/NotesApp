using AutoMapper;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Note.Responses;
using NotesApp.ApplicationCore.Extensions;
using NotesApp.Domain.Errors.Exceptions;
using NotesApp.Domain.Interfaces;

namespace NotesApp.ApplicationCore.Notes.Queries.GetNoteDetailById;

public class GetNoteDetailByIdQueryHandler : NoteBase, IRequestHandler<GetNoteDetailByIdQuery,Result<GetNoteDetailResponse>>
{
    private readonly INoteRepository _noteRepository;
    private readonly IMapper _mapper;
    public GetNoteDetailByIdQueryHandler(ISender mediator, INoteRepository noteRepository, IMapper mapper) : base(mediator)
    {
        _noteRepository = noteRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetNoteDetailResponse>> Handle(GetNoteDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var noteId = await GetNoteId(cancellationToken);
        
        if (noteId == null)
            return new Result<GetNoteDetailResponse>().CreateException<GetNoteDetailResponse,NoteNotFoundException>();

        var note = await _noteRepository.GetById(noteId);

        var noteDetail = note.NoteDetails.FirstOrDefault(n => n.NoteDetailId == request.NoteDetailId);
        
        if (noteDetail == null)
            return new Result<GetNoteDetailResponse>().CreateException<GetNoteDetailResponse,NoteNotFoundException>();
        
        var noteDetailDto = _mapper.Map<GetNoteDetailResponse>(noteDetail);
        
        return noteDetailDto;
    }
}