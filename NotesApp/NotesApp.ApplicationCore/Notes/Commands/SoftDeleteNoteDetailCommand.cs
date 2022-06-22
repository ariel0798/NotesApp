using MediatR;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.Commands;

public class SoftDeleteNoteDetailCommand : IRequest<NoteDetail>
{
    public string NoteId { get; set; }
    public string NoteDetailId { get; set; }
}