using FluentValidation;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.ApplicationCore.Services.AuthService;
using NotesApp.ApplicationCore.Services.NoteServices;

namespace NotesApp.ApplicationCore;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddSingleton<IAuthService,AuthService>();
        services.AddSingleton<INoteService,NoteService>();
        
        services.AddAutoMapper(typeof(DependencyInjection)); 

        services.AddMediatR(typeof(DependencyInjection).Assembly);

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly,ServiceLifetime.Transient);

        services.AddHangfire(h => h.UseSqlServerStorage(configuration.GetConnectionString("HangfireSqlServer")));
        services.AddHangfireServer();
        return services;
    }
}