using Microsoft.AspNetCore.Mvc.Filters;

namespace EVChargingManagementAPI.Filters
{
    public class ApiLoggingFilter:IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;
        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("API Execution Startes: {controller}.{Action}",
                context.Controller.GetType().Name,
                context.ActionDescriptor.DisplayName);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("API Execution Completed: {controller}.{Action}",
               context.Controller.GetType().Name,
               context.ActionDescriptor.DisplayName);
        }
    }
}
