using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.ApplicationCore.Services.AuthService;
using NotesApp.ApplicationCore.Services.NoteServices;

namespace NotesApp.ApplicationCore;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddSingleton<IAuthService,AuthService>();
        services.AddSingleton<INoteService,NoteService>();
        
        services.AddAutoMapper(typeof(DependencyInjection)); 

        services.AddMediatR(typeof(DependencyInjection).Assembly);

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly,ServiceLifetime.Transient);
        return services;
    }
}