using MediatR;
using NotesApp.ApplicationCore.Notes.Queries;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.QueryHandlers;

public class GetNoteDetailByIdQueryHandler : IRequestHandler<GetNoteDetailByIdQuery,NoteDetail>
{
    private readonly INoteRepository _noteRepository;

    public GetNoteDetailByIdQueryHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    
    public async Task<NoteDetail> Handle(GetNoteDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetById(request.NoteId);
        return note.Notes.FirstOrDefault(n => n.NoteDetailId.ToString() == request.NoteDetailId);
    }
}