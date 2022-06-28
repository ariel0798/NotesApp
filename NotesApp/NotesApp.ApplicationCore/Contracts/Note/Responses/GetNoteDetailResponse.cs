namespace NotesApp.ApplicationCore.Contracts.Note.Responses;

public class GetNoteDetailResponse
{
    public string NoteDetailId { get; set; } 
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime LastEdited { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public DateTime? NoteDeleted { get; set; } = null;
}