using NotesApp.Api.Extensions;
using NotesApp.Api.Middlewares;
using NotesApp.Infrastructure;
using NotesApp.ApplicationCore;

var builder = WebApplication.CreateBuilder(args);

builder
    .RegisterServices<Program>();

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseEndpoints<Program>();

app.Run();