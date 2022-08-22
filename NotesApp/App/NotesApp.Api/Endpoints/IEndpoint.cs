namespace NotesApp.Api.Endpoints;

public interface IEndpoint
{
    public static abstract void DefineEndpoint(IEndpointRouteBuilder app);
}