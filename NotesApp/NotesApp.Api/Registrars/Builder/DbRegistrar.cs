using Redis.OM;

namespace NotesApp.Api.Registrars.Builder;

public class DbRegistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton(opt => 
            new RedisConnectionProvider(builder.Configuration.GetConnectionString("RedisConnectionString")));

    }
}