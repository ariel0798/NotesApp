using MediatR;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.Commands;

public class UpdateNoteDetailCommand : IRequest<NoteDetail>
{
    public string NoteId { get; set; }
    public string NoteDetailId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}