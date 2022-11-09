using NotesApp.ApplicationCore;
using NotesApp.Infrastructure.Data;
using NotesApp.MinApi.Extensions;
using NotesApp.MinApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.RegisterServices(typeof(Program));
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
        
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints<Program>();

app.Run();