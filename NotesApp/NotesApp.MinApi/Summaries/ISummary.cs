using NotesApp.MinApi.Endpoints;

namespace NotesApp.MinApi.Summaries;

public interface ISummary<T>
where T : IEndpoint
{
    public static abstract RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder);
}