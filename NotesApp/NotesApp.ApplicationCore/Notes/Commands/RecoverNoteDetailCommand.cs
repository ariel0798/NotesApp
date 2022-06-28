using MediatR;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.Commands;

public class RecoverNoteDetailCommand : IRequest<NoteDetail>
{
    public string NoteId { get; set; }
    public string NoteDetailId { get; set; }
}