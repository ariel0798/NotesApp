namespace NotesApp.ApplicationCore.Contracts.ErrorResponses;

public class ValidationResponse 
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get;  } 
    public Errors[] Errors { get; }
}

public class Errors
{
    public string[] Field { get; }
}