
namespace UmbracoPOCV13.HealthCheckHandlersMiddleware
{
    /// <summary>
    /// Checks if your site is allowed to be IFRAMEd by another site and thus would be susceptible to click-jacking.
    /// </summary>
    public class ClickJackingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.Headers.Append("X-Frame-Options", "SAMEORIGIN");
            await next(context);
        }
    }
}
