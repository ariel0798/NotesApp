using Redis.OM;
using StackExchange.Redis;

namespace NotesApp.MinApi.Registrars.Services;

public class DbRegistrar : IRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton(opt => 
            new RedisConnectionProvider(builder.Configuration.GetConnectionString("RedisConnectionString")));
        builder.Services.AddSingleton<IConnectionMultiplexer>(opt =>
            ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnectionStringIp")));

    }
}