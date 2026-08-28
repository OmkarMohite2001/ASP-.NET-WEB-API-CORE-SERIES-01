namespace EVChargingManagementAPI.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var startTime = DateTime.UtcNow;
            _logger.LogInformation("Request Started : {Method} {Path}",
                context.Request.Method,
                context.Request.Path);
            await _next(context);
            var duration = DateTime.UtcNow - startTime;

            _logger.LogInformation("Request Completed : {StatusCode} {Duration}",
                context.Response.StatusCode,
                duration.TotalMilliseconds);
        }
    }
}
