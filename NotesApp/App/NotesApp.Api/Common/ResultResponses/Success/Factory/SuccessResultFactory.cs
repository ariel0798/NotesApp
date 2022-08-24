using System.Collections.Immutable;

namespace NotesApp.Api.Common.ResultResponses.Success.Factory;

public class SuccessResultFactory
{
    private readonly IReadOnlyDictionary<int, ISuccessResult> _successResults;
    public SuccessResultFactory(string url)
    {
        var failureResultType = typeof(ISuccessResult);
        _successResults = failureResultType.Assembly.ExportedTypes
            .Where(x => failureResultType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(x =>
            {
                var parameterlessCtor = x.GetConstructors().SingleOrDefault(c => c.GetParameters().Length == 0);
                return  parameterlessCtor is not null
                     ? Activator.CreateInstance(x)
                     : Activator.CreateInstance(x, url);
                
            })
            .Cast<ISuccessResult>()
            .ToImmutableDictionary(x => x.StatusCode, x => x);
    }

    public ISuccessResult GetSuccessResultByStatusCode(int statusCode)
    {
        var response =_successResults.GetValueOrDefault(statusCode);
        return response ?? DefaultSuccessResult();
    }

    private ISuccessResult DefaultSuccessResult()
    {
        return _successResults[default(int)];
    }
}