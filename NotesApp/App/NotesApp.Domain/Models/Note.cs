namespace NotesApp.Domain.Models;

public class Note
{
    public Note()
    {
        NoteDetails = new HashSet<NoteDetail>();
    }

    public int NoteId { get; set; }
    public int? UserId { get; set; }

    public virtual User? User { get; set; }
    public virtual ICollection<NoteDetail> NoteDetails { get; set; }
}