using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace ActionFilterImplementation.Filters
{
    // Custom action filter implementing synchronous IActionFilter interface
    public class RequestResponseLoggingFilter : IActionFilter
    {
        private readonly ILogger<RequestResponseLoggingFilter> _logger;

        // Constructor receives ILogger<RequestResponseLoggingFilter> via Dependency Injection
        public RequestResponseLoggingFilter(ILogger<RequestResponseLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;

            var request = httpContext.Request;

            // Serialize the query string parameters to JSON string for logging
            var query = JsonSerializer.Serialize(request.Query);

            // Serialize the route data (like URL parameters) to JSON string for logging
            var routeData = JsonSerializer.Serialize(context.RouteData.Values);

            // Serialize all HTTP request headers to JSON string for logging
            var headers = JsonSerializer.Serialize(request.Headers);

            // Log detailed information about the incoming HTTP request
            // Including HTTP method (GET, POST, etc.), request path, query parameters, route data, and headers
            _logger.LogInformation($"Request Incoming: Method={request.Method}, Path={request.Path}, Query={query}, RouteData={routeData}, Headers={headers}");

        }

        // This method executes immediately after the action method has run
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Get HttpContext from the current action context
            var httpContext = context.HttpContext;

            // Get the HTTP response that will be sent to the client
            var response = httpContext.Response;

            // Serialize all HTTP response headers to JSON string for logging
            var headers = JsonSerializer.Serialize(response.Headers);

            // Log detailed information about the outgoing HTTP response
            // Including HTTP status code and response headers
            _logger.LogInformation($"Response Outgoing: StatusCode={response.StatusCode}, Headers={headers}");
        }
    }
}
