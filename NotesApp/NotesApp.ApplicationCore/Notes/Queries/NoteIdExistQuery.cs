using MediatR;

namespace NotesApp.ApplicationCore.Notes.Queries;

public class NoteIdExistQuery : IRequest<bool>
{
    public string Id { get; set; }
}