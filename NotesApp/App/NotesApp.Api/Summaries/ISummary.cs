using NotesApp.Api.Endpoints;

namespace NotesApp.Api.Summaries;

public interface ISummary<T>
    where T : IEndpoint
{
    public static abstract RouteHandlerBuilder SetSummary(RouteHandlerBuilder builder);
}