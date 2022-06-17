using MediatR;
using NotesApp.ApplicationCore.Notes.Queries;
using NotesApp.Domain.Interfaces;

namespace NotesApp.ApplicationCore.Notes.QueryHandlers;

public class NoteIdExistQueryHandler : IRequestHandler<NoteIdExistQuery,bool>
{
    private readonly INoteRepository _noteRepository;

    public NoteIdExistQueryHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    public async Task<bool> Handle(NoteIdExistQuery request, CancellationToken cancellationToken)
    {
        return await _noteRepository.GetById(request.Id) != null ? true : false;
    }
}