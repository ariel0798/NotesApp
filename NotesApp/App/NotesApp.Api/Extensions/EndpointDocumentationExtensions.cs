using Swashbuckle.AspNetCore.Annotations;

namespace NotesApp.Api.Extensions;

public static class EndpointDocumentationExtensions
{
    public static RouteHandlerBuilder Response<TResponse> (this RouteHandlerBuilder builder, int statusCode, string description)
    {
        return builder
            .WithMetadata(new SwaggerResponseAttribute(
                statusCode: statusCode,
                description: description,
                type: typeof(TResponse)));
    }
    
    public static RouteHandlerBuilder Summary (this RouteHandlerBuilder builder, string summary, string description)
    {
        return builder
            .WithMetadata(new SwaggerOperationAttribute(
                summary: summary,
                description: description));
    }
}