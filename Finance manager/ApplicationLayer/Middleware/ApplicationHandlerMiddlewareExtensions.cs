namespace Server.Middleware;

public static class ApplicationHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseApplicationMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ApplicationExceptionHandlerMiddleware>();

        return builder;
    }
}
