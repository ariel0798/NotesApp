namespace NotesApp.Api.Common.ResultResponses.Success;

public interface ISuccessResult
{
    IResult GetSuccessResult(object obj, string url = "");
    
    public int StatusCode { get; }
}