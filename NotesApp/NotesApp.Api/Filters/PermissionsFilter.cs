using Microsoft.AspNetCore.Mvc.Filters;

namespace NotesApp.Api.Filters;

public class PermissionsFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        Console.WriteLine("Executing");    
    }
}