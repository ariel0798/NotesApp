using Redis.OM;
using StackExchange.Redis;

namespace NotesApp.Api.Registrars.Builder;

public class DbRegistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton(opt => 
            new RedisConnectionProvider(builder.Configuration.GetConnectionString("RedisConnectionString")));
        builder.Services.AddSingleton<IConnectionMultiplexer>(opt =>
            ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnectionStringIp")));

    }
}