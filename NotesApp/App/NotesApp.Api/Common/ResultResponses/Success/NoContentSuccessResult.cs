namespace NotesApp.Api.Common.ResultResponses.Success;

public class NoContentSuccessResult : ISuccessResult
{
    public IResult GetSuccessResult(object obj, string url = "")
    {
        return Results.NoContent();
    }

    public int StatusCode => StatusCodes.Status204NoContent;
}