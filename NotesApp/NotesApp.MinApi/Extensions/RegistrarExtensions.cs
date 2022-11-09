using NotesApp.MinApi.Registrars;

namespace NotesApp.MinApi.Extensions;

public static class RegistrarExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder, Type scanningType)
    {
        var registrars = GetRegistrars(scanningType);

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