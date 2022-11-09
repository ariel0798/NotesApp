using NotesApp.Api.Filters;

namespace NotesApp.Api.Registrars.Builder;

public class MvcRegistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(options => options.Filters.Add<PermissionsFilter>());
        builder.Services.AddEndpointsApiExplorer();
    }
}