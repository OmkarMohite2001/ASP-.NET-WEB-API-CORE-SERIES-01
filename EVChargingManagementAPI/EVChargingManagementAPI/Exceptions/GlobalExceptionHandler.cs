using EVChargingManagementAPI.DTOs;
using Microsoft.AspNetCore.Http;
namespace EVChargingManagementAPI.Exceptions

{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occured");
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var response = new ErrorResponseDto
                {
                    StatusCode = 500,
                    Message = "Something Went Wrong. Please Try Again."
                };
                
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
