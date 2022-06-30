using NotesApp.Domain;
using NotesApp.Domain.Interfaces;
using NotesApp.Infrastructure.Data;

namespace NotesApp.Api.Registrars.Builder;

public class InfrastructureRegistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.Scan(scan => scan 
            .FromAssembliesOf(typeof(InfrastructureEntryPoint), typeof(DomainEntryPoint))
            .AddClasses()
            .AsImplementedInterfaces()
            .WithSingletonLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(IBaseRepository<>)))
            .AsSelf()
            .WithSingletonLifetime()
        );
    }
}