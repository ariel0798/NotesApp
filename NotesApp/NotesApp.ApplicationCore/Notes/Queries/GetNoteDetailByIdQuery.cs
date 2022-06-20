using MediatR;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.Queries;

public class GetNoteDetailByIdQuery : IRequest<NoteDetail>
{
    public string NoteId { get; set; }
    public string NoteDetailId { get; set; }
}