namespace NotesApp.Domain.Models;

public class NoteDetail
{
    public string NoteDetailId { get; } = Guid.NewGuid().ToString();
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime LastEdited { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public DateTime? NoteDeleted { get; set; } = null;
}