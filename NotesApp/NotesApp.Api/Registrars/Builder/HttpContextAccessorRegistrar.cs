namespace NotesApp.Api.Registrars.Builder;

public class HttpContextAccessorRegistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
    }
}