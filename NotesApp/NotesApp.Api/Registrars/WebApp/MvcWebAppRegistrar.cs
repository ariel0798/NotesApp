using NotesApp.Api.Middlewares;

namespace NotesApp.Api.Registrars.WebApp;

public class MvcWebAppRegistrar :  IWebApplicationRegistrar
{
    public void RegisterPipelineComponents(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();
    }
}