using LanguageExt.Common;
using NotesApp.Api.Common.ResultResponses.Failures.Factory;

namespace NotesApp.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToOk<TResult>(
        this Result<TResult> result, bool isCreatedResource = false, string uri = "" )
    {
        return result.Match<IResult>(obj 
                =>
            {
                if (isCreatedResource)
                    return Results.Created(uri, obj);
                else
                    return Results.Ok(obj);
            }, 
            exception =>
            {
                var failureResultFactory = new FailureResultFactory();

                var failureResult = failureResultFactory.GetFailureResultByType(exception.GetType());

                return failureResult.GetFailureResult(exception);
            });
    }
}