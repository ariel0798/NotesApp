namespace NotesApp.Api.Common.ResultResponses.Failures;

public interface IFailureResult
{
    IResult GetFailureResult(Exception e);
    
    public Type ExceptionType { get; }
}