namespace EmployeeAPI.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("--------------------------------");

            Console.WriteLine("Request Recieved");

            Console.WriteLine($"URL : {context.Request.Path}");

            Console.WriteLine($"Method : {context.Request.Method}");

            await _next(context);

            Console.WriteLine($"Status Code : {context.Response.StatusCode}");

            Console.WriteLine("Response Sended");

            Console.WriteLine("--------------------------------");
        }
    }
}
