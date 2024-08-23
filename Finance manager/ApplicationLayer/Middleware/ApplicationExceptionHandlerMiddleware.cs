using System.Net;
using System.Text.Json;

namespace ApplicationLayer.Middleware;

public class ApplicationExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ApplicationExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (exception)
        {
            default:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(exception.Message);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
            result = JsonSerializer.Serialize(new { error = exception.Message });

        return context.Response.WriteAsync(result);
    }
}
