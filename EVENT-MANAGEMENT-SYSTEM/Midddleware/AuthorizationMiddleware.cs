using System.IO;

namespace EVENT_MANAGEMENT_SYSTEM.Midddleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            // Allowing swagger and scalar UI
            var path = context.Request.Path.Value;
            if (path.StartsWith("/api/Auth/login") ||
        path.StartsWith("/api/Auth/register") ||
        path.StartsWith("/swagger") ||
        path.StartsWith("/scalar"))
            {
                await _next(context);
                return;
            }
            var user = context.User;
            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsJsonAsync(new
                {
                    message = "Unauthorized access"
                });
            }
        }
    }
}
