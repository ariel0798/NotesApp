using AutoMapper;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Note.Responses;
using NotesApp.ApplicationCore.Extensions;
using NotesApp.Domain.Errors.Exceptions;
using NotesApp.Domain.Interfaces;

namespace NotesApp.ApplicationCore.Notes.Queries.GetAllNoteDetailsTrash;

public class GetAllNoteDetailsTrashQueryHandler : NoteBase, IRequestHandler<GetAllNoteDetailsTrashQuery,Result<List<GetNoteDetailResponse>>>
{
    private readonly INoteRepository _noteRepository;
    private readonly IMapper _mapper;
    public GetAllNoteDetailsTrashQueryHandler(ISender mediator, INoteRepository noteRepository, IMapper mapper) 
        : base(mediator)
    {
        _noteRepository = noteRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<GetNoteDetailResponse>>> Handle(GetAllNoteDetailsTrashQuery request, CancellationToken cancellationToken)
    {
        var noteId = await GetNoteId(cancellationToken);        
        if (noteId == null)
            return new Result<List<GetNoteDetailResponse>>().CreateException<List<GetNoteDetailResponse>,NoteNotFoundException>();


        var note = await _noteRepository.GetById(noteId);

        var noteDetailDeleted = note.NoteDetails.Where(n => n.IsDeleted == true);
        var noteDetailsDto = _mapper.Map<List<GetNoteDetailResponse>>(noteDetailDeleted);
        return noteDetailsDto;
    }
}