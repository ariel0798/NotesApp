namespace NotesApp.MinApi.Registrars.Services;

public class MappingRegistrar : IRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(Program).Assembly);
    }
}