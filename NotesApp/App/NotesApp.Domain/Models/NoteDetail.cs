namespace NotesApp.Domain.Models;

public class NoteDetail
{
    public int NoteDetailId { get; set; }
    public int? NoteId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime? LastEdited { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }

    public virtual Note? Note { get; set; }
}