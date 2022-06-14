namespace NotesApp.Domain.Models;

public class NoteDetail
{
    public Guid NoteDetailId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime LastEdited { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public DateTime NoteDeleted { get; set; }
}