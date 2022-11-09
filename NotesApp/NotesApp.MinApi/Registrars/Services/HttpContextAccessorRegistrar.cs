namespace NotesApp.MinApi.Registrars.Services;

public class HttpContextAccessorRegistrar : IRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
    }
}