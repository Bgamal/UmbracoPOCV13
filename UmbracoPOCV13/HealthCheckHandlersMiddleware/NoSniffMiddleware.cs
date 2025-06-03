namespace UmbracoPOCV13.HealthCheckHandlersMiddleware
{
    /// <summary>
    /// Protect your Umbraco site from MIME sniffing vulnerabilities using security headers like X-Content-Type-Options.
    /// </summary>
    public class NoSniffMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
            await next(context);
        }
    }
}
