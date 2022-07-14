using AutoMapper;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Note.Responses;
using NotesApp.ApplicationCore.Extensions;
using NotesApp.Domain.Errors.Exceptions;
using NotesApp.Domain.Interfaces;

namespace NotesApp.ApplicationCore.Notes.Queries.GetAllNoteDetails;

public class GetAllNoteDetailsQueryHandler : NoteBase, IRequestHandler<GetAllNoteDetailsQuery,Result<List<GetNoteDetailResponse>>>
{
    private readonly INoteRepository _noteRepository;
    private readonly IMapper _mapper;
    
    public GetAllNoteDetailsQueryHandler(ISender mediator, INoteRepository noteRepository, IMapper mapper) 
        : base(mediator)
    {
        _noteRepository = noteRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<List<GetNoteDetailResponse>>> Handle(GetAllNoteDetailsQuery request, CancellationToken cancellationToken)
    {
        var noteId = await GetNoteId(cancellationToken);
        
        if (noteId == null)
            return new Result<List<GetNoteDetailResponse>>().CreateException<List<GetNoteDetailResponse>,NoteNotFoundException>();

        var note = await _noteRepository.GetById(noteId);

        var noteDetailNonDeleted = note.NoteDetails.Where(n => n.IsDeleted == false);
        var noteDetailsDto = _mapper.Map<List<GetNoteDetailResponse>>(noteDetailNonDeleted);
        return noteDetailsDto;
    }
}