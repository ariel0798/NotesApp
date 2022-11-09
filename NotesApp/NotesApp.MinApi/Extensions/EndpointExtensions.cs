using System.Reflection;
using NotesApp.MinApi.Endpoints;
using NotesApp.MinApi.Summaries;

namespace NotesApp.MinApi.Extensions;

public static class EndpointExtensions
{
    public static void UseEndpoints<TMarker>(this IApplicationBuilder app)
    {
        UseEndpoints(app, typeof(TMarker));
    }

    private static void UseEndpoints(this IApplicationBuilder app, Type typeMarker)
    {
        var endpointTypes = GetEndpointTypesFromAssemblyContaining(typeMarker);

        foreach (var endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpoint.DefineEndpoints))!
                .Invoke(null, new object[] { app });
        }
        
    }

    private static IEnumerable<TypeInfo> GetEndpointTypesFromAssemblyContaining(Type typeMarker)
    {
        var endpointTypes = typeMarker.Assembly.DefinedTypes
            .Where(x => !x.IsAbstract && !x.IsInterface &&
                        typeof(IEndpoint).IsAssignableFrom(x));
        return endpointTypes;
    }

    public static RouteHandlerBuilder FindSummary<TEndpoint>(this RouteHandlerBuilder route)
    where TEndpoint : IEndpoint
    {
        
        var endpointSummary = typeof(Program).Assembly.DefinedTypes
            .Where(x => !x.IsAbstract && !x.IsInterface &&
                        typeof(ISummary<TEndpoint>).IsAssignableFrom(x));

        if (endpointSummary.Length() > 0)
        {
            var summary = endpointSummary.First();
            
            return  (RouteHandlerBuilder)summary.GetMethod(nameof(ISummary<TEndpoint>.SetSummary))!
                .Invoke(null, new object[] { route })!;
        }

        return route;
    }
}