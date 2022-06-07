namespace NotesApp.Api.Registrars;

public interface IWebApplicationRegistrar
{
    public void RegisterPipelineComponents(WebApplication app);
}