namespace NotesApp.ApplicationCore.Contracts.Notes.Responses;

public class NoteDetailResponse
{
    public string NoteDetailId { get; set; } 
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime LastEdited { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedDate { get; set; } = null;
}