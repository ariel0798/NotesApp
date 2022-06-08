using NotesApp.ApplicationCore;

namespace NotesApp.Api.Registrars.Builder;

public class ServicesRegistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.Scan(scan => scan
            .FromAssembliesOf(typeof(ApplicationCoreEntryPoint))
            .AddClasses()
            .AsImplementedInterfaces()
            .WithSingletonLifetime()
        );
    }
}