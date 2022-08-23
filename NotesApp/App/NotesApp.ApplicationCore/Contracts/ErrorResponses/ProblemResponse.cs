namespace NotesApp.ApplicationCore.Contracts.ErrorResponses;

public class ProblemResponse
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get;  } 
}