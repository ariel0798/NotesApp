using MediatR;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.Queries;

public class GetNoteByIdQuery : IRequest<Note>
{
    public string NoteId { get; set; }
}