namespace NotesApp.Api.Common.ResultResponses.Success;

public class DefaultSuccessResult : ISuccessResult
{
    public IResult GetSuccessResult(object obj, string url = "")
    {
        return Results.Ok(obj);
    }

    public int StatusCode => default(int);
}