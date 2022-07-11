using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.ApplicationCore.Authentication;
using NotesApp.ApplicationCore.Authentication.Interfaces;
using NotesApp.Domain.Interfaces;
using NotesApp.Infrastructure.Data.Authentication;
using NotesApp.Infrastructure.Data.Repositories;


namespace NotesApp.Infrastructure.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
         ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<INoteRepository, NoteRepository>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        
        return services;
    }
}