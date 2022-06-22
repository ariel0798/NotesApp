using System.Security.AccessControl;

namespace NotesApp.ApplicationCore.Dtos.Note;

public class UpdateNoteDetailDto
{
    public string NoteDetailId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}