using MediatR;
using NotesApp.ApplicationCore.Notes.Queries;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.QueryHandlers;

public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery,Note>
{
    private readonly INoteRepository _noteRepository;

    public GetNoteByIdQueryHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    public Task<Note> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        return _noteRepository.GetById(request.NoteId);
    }
}