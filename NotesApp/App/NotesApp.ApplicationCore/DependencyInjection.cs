using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace NotesApp.ApplicationCore;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddAutoMapper(typeof(DependencyInjection)); 

        services.AddMediatR(typeof(DependencyInjection).Assembly);

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection),ServiceLifetime.Transient);
        
        return services;
    }
}