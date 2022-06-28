namespace NotesApp.ApplicationCore.Contracts.Note.Requests;

public class CreateNoteRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
}