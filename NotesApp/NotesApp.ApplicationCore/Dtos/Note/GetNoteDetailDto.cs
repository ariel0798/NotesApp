namespace NotesApp.ApplicationCore.Dtos.Note;

public class GetNoteDetailDto
{
    public string NoteDetailId { get; set; } 
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime LastEdited { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public DateTime? NoteDeleted { get; set; } = null;
}