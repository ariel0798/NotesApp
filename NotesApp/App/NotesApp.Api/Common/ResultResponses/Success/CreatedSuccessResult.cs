namespace NotesApp.Api.Common.ResultResponses.Success;

public class CreatedSuccessResult : ISuccessResult
{
    private readonly string _url;
    
    public CreatedSuccessResult(string url)
    {
        _url = url;
    }
    
    public IResult GetSuccessResult(object obj, string url = "")
    {
        return Results.Created(_url, obj);
    }

    public int StatusCode => StatusCodes.Status201Created;
}