using LanguageExt.Common;
using NotesApp.Api.Common.ResultResponses.Failures.Factory;
using NotesApp.Api.Common.ResultResponses.Success.Factory;

namespace NotesApp.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToOk<TResult>(
        this Result<TResult> result,  int statusCode = default(int), string url = "")
    {
        return result.Match<IResult>(obj 
                =>
            {
                var successResultFactory = new SuccessResultFactory(url);

                var successResult = successResultFactory.GetSuccessResultByStatusCode(statusCode);

                return successResult.GetSuccessResult(obj);
            }, 
            exception =>
            {
                var failureResultFactory = new FailureResultFactory();

                var failureResult = failureResultFactory.GetFailureResultByType(exception.GetType());

                return failureResult.GetFailureResult(exception);
            });
    }
}