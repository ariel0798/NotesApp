namespace NotesApp.MinApi.Endpoints;

public interface IEndpoint
{
    public static abstract void DefineEndpoints(IEndpointRouteBuilder app);
}