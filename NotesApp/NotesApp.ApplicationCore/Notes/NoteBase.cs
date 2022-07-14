using MediatR;
using NotesApp.ApplicationCore.Notes.Queries.GetNoteIdByUserEmail;

namespace NotesApp.ApplicationCore.Notes;

public abstract class NoteBase
{
    private readonly ISender _mediator;

    public NoteBase(ISender mediator)
    {
        _mediator = mediator;
    }
    
    protected async Task<string?> GetNoteId(CancellationToken cancellationToken)
    {
        var query = new GetNoteIdByUserEmailQuery();
        return await _mediator.Send(query,cancellationToken);
    }
    
}