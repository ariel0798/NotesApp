using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore;
using NotesApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
{
    builder.RegisterServices(typeof(Program));
    builder.Services
        .AddInfrastructure(builder.Configuration)
        .AddApplication();
}


var app = builder.Build();

app.RegisterPipelineComponents(typeof(Program));

app.Run();