using System.Collections.Immutable;

namespace NotesApp.Api.Common.ResultResponses.Failures.Factory;

public class FailureResultFactory
{
    private readonly IReadOnlyDictionary<Type, IFailureResult> _failureResults;
    public FailureResultFactory()
    {
        var failureResultType = typeof(IFailureResult);
        _failureResults = failureResultType.Assembly.ExportedTypes
            .Where(x => failureResultType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(x =>
            {
                var parameterlessCtor = x.GetConstructors().SingleOrDefault(c => c.GetParameters().Length == 0);
                return  Activator.CreateInstance(x);
            })
            .Cast<IFailureResult>()
            .ToImmutableDictionary(x => x.ExceptionType, x => x);
    }

    public IFailureResult GetFailureResultByType(Type type)
    {
        var response = _failureResults.GetValueOrDefault(type);
        return response ?? DefaultFailureResult();
    }

    private IFailureResult DefaultFailureResult()
    {
        return _failureResults[typeof(Type)];
    }
}