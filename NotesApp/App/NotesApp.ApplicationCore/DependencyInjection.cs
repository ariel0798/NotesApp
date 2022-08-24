using FluentValidation;
using HashidsNet;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.ApplicationCore.Common.Models;
namespace NotesApp.ApplicationCore;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, 
        ConfigurationManager configuration)
    {

        services.AddAutoMapper(typeof(DependencyInjection)); 

        services.AddMediatR(typeof(DependencyInjection).Assembly);

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection),ServiceLifetime.Transient);

        services.AddHashId(configuration);

        services.AddHttpContextAccessor();

        return services;
    }

    private static IServiceCollection AddHashId(this IServiceCollection services, ConfigurationManager configuration)
    {
        var hashSettings = new
        {
            Salt = configuration.GetSection("HashIdSettings").GetSection("Salt").Value
            ,MinimumHashIdLenght = Int32.Parse(configuration.GetSection("HashIdSettings")
                .GetSection("MinimumHashIdLenght").Value!)
        };
        
        services.AddSingleton<IHashids>(_ => new Hashids(hashSettings.Salt, hashSettings.MinimumHashIdLenght));
        
        services.Configure<HashIdSettings>(configuration.GetSection(HashIdSettings.SectionName));

        return services;
    }
}