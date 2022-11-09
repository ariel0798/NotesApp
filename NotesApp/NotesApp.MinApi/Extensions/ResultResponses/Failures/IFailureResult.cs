namespace NotesApp.MinApi.Extensions.ResultResponses.Failures;

public interface IFailureResult
{
    IResult GetFailureResult(Exception e);
    
    public Type ExceptionType { get; }
}