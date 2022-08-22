using NotesApp.Api.Extensions;
using NotesApp.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder
    .RegisterServices<Program>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseEndpoints<Program>();

app.Run();