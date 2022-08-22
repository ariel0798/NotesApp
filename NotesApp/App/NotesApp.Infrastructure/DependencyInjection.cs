using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.ApplicationCore.Authentication.Interfaces;
using NotesApp.Domain.Interfaces;
using NotesApp.Infrastructure.Authentication;
using NotesApp.Infrastructure.Context;
using NotesApp.Infrastructure.DatabaseProvider;
using NotesApp.Infrastructure.Repositories;
using NotesApp.Infrastructure.Services;
using NotesApp.Infrastructure.Services.Interfaces;

namespace NotesApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        ConfigurationManager configuration
        )
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        
        services.AddScoped<IDatabaseProvider, DatabaseProvider.DatabaseProvider>();
        services.AddDbContext<NotesAppDbContext>(confi =>
            confi.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        
        return services;
    }
}