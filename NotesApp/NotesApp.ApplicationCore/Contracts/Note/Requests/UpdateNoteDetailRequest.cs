namespace NotesApp.ApplicationCore.Contracts.Note.Requests;

public class UpdateNoteDetailRequest
{
    public string NoteDetailId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}