using FluentValidation;
using HashidsNet;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NotesApp.ApplicationCore;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, 
        ConfigurationManager configuration)
    {

        services.AddAutoMapper(typeof(DependencyInjection)); 

        services.AddMediatR(typeof(DependencyInjection).Assembly);

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection),ServiceLifetime.Transient);

        services.AddSingleton<IHashids>(_ => 
            new Hashids(configuration.GetSection("HashIdSalt").Key,11));
        return services;
    }
}