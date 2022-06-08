using NotesApp.ApplicationCore;

namespace NotesApp.Api.Registrars.Builder;

public class AutoMapperRegistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(ApplicationCoreEntryPoint)); 
        builder.Services.AddControllersWithViews();
    }
}