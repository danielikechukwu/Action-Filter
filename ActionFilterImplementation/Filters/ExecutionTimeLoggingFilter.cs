using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ActionFilterImplementation.Filters
{
    public class ExecutionTimeLoggingFilter : ActionFilterAttribute
    {
        // Stopwatch instance to measure elapsed time; nullable to allow re-initialization
        private Stopwatch? _stopwatch;

        private readonly ILogger<ExecutionTimeLoggingFilter> _logger;

        public ExecutionTimeLoggingFilter(ILogger<ExecutionTimeLoggingFilter> logger)
        {
            _logger = logger;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Initialize and start the stopwatch when the action starts executing
            _stopwatch = Stopwatch.StartNew();

            var executedContext = await next();

            // Stop the Stopwatch after action execution completes
            _stopwatch.Stop();

            var actionName = context.ActionDescriptor.DisplayName;

            // Log the elapsed time in milliseconds for the action execution
            _logger.LogInformation($"Execution Time for {actionName} : {_stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
