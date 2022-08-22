using NotesApp.Api.Registrars;

namespace NotesApp.Api.Extensions;

public static class RegistrarExtensions
{
    public static void RegisterServices<TMarker>(this WebApplicationBuilder builder)
    {
        RegisterServices(builder,typeof(TMarker));
    }

    private static void RegisterServices(this WebApplicationBuilder builder, Type typeMarker)
    {
        var registrars = GetRegistrars(typeMarker);

        foreach (var registrar in registrars)
        {
            registrar.RegisterServices(builder);
        }
    }
    
    private static IEnumerable<IRegistrar> GetRegistrars(Type scanningType)
    {
        return scanningType.Assembly.GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IRegistrar)) && !t.IsAbstract && !t.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<IRegistrar>();
    }
}