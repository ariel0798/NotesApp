using MediatR;
using NotesApp.ApplicationCore;

namespace NotesApp.Api.Registrars.Builder;

public class MediatorRegistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(typeof(ApplicationCoreEntryPoint).Assembly);
    }
}