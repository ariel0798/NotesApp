using NotesApp.Api.Extensions;
using NotesApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddInfrastructure(builder.Configuration);
    builder.RegisterServices(typeof(Program));
    
}


var app = builder.Build();

app.RegisterPipelineComponents(typeof(Program));

app.Run();