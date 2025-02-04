using System.Net;
using System.Text.Json;

namespace Server.Middleware;

public class ApplicationExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApplicationExceptionHandlerMiddleware> _logger;

    public ApplicationExceptionHandlerMiddleware(RequestDelegate next, ILogger<ApplicationExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
        var result = JsonSerializer.Serialize(exception.Message);

        switch (exception)
        {
            case UnauthorizedAccessException accessException:
                code = HttpStatusCode.Unauthorized;

                _logger.LogWarning("Unauthorized access attempt: \n message: {@Message}\n code: {Code}", exception.Message, (int)code);
                break;
            default:
                code = HttpStatusCode.BadRequest;

                _logger.LogError("Throws exception:\n message: {@Message}\n code: {@Code}", exception.Message, (int)code);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}
