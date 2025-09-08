using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilterImplementation.Filters
{
    public class APIResponseWrapperFilter : ActionFilterAttribute
    {
        // This method runs after the action method has executed
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Check if the action returned an ObjectResult (typical JSON/data response)
            if(context.Result is ObjectResult objectResult)
            {
                // Create a new anonymous object to wrap the original response value
                // Standardizing the response format to include Status, Message, and Data fields
                var wrappedResponse = new
                {
                    Status = objectResult.StatusCode ?? 200, // Default to 200 if null
                    Message = "Request processed successfully",
                    Data = objectResult.Value // Original response data
                };

                context.Result = new ObjectResult(wrappedResponse)
                {
                    StatusCode = objectResult.StatusCode // Preserve the original status code
                };
            }
            // If the action returned an EmptyResult (no content to send)
            else if (context.Result is EmptyResult)
            {
                var wrappedResponse = new
                {
                    Status = 204, // No Content
                    Message = "No content available",
                    Data = (object?)null // No data
                };

                context.Result = new ObjectResult(wrappedResponse)
                {
                    StatusCode = 204 // No Content status code
                };
            }

            base.OnActionExecuted(context); // Call the base method
        }
    }
}
