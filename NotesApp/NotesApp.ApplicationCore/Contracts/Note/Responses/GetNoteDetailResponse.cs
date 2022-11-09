namespace NotesApp.ApplicationCore.Contracts.Note.Responses;

public class GetNoteDetailResponse
{
    public int NoteDetailId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime? LastEdited { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }
}