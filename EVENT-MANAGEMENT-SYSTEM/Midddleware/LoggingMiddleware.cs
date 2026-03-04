using System.IO;

namespace EVENT_MANAGEMENT_SYSTEM.Midddleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var methodName = context.Request.Method;
            var urlPath= context.Request.Path;

            _logger.LogInformation($"Incoming Request: {methodName} {urlPath}");

            var requestStartTime = DateTime.UtcNow;

            await _next(context);

            var requestDuration = DateTime.UtcNow - requestStartTime;

            _logger.LogInformation($"Status of Respone is : {context.Response.StatusCode} || Duration : {requestDuration.Milliseconds}ms");
        }
    }
}
