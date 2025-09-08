using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ActionFilterImplementation.Filters
{
    public class ComplexInputValidationFilter : IAsyncActionFilter
    {
        // This method runs asynchronously before and after the action method execution
        // 'context' gives info about the current request and action arguments
        // 'next' delegate is used to invoke the next action filter or action method itself
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Try to get the "StartDate" argument from the action method parameters
            // and assign it to 'startDateObj' if it exists
            // Similarly, try to get "EndDate" argument and assign to 'endDateObj'
            if (context.ActionArguments.TryGetValue("StartDate", out var startDateObj) && context.ActionArguments.TryGetValue("EndDate", out var endDateObj) && startDateObj is DateTime startDate && endDateObj is DateTime endDate)
            {
                if (startDate > endDate)
                {
                    context.Result = new BadRequestObjectResult(new
                    {
                        Status = 400,
                        Message = "StartDate cannot be later than EndDate."
                    });

                    // Short-circuit the pipeline here, preventing the action method from executing
                    return;
                }
            }

            await next(); // Proceed to the next action filter or action method
        }
    }
}
