using ActionFilterImplementation.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActionFilterImplementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        // This endpoint demonstrates Request and Response Logging
        // The RequestResponseLoggingFilter is applied here using TypeFilter attribute for dependency injection support
        [HttpGet("log-request-response")]
        [TypeFilter(typeof(RequestResponseLoggingFilter))]
        public IActionResult LogRequestResponse([FromQuery] string sampleParam)
        {
            // Return HTTP 200 OK with a simple JSON response confirming logging worked and echoing the query param
            return Ok(new { Message = "Request and response logged successfully.", Param = sampleParam });
        }

        // This endpoint demonstrates Input Validation filter usage
        // ComplexInputValidationFilter applied via TypeFilter attribute for DI
        [HttpGet("validate-input")]
        [TypeFilter(typeof(ComplexInputValidationFilter))]
        public IActionResult ValidateInput([FromQuery] DateTime StartDate, [FromQuery] DateTime EndDate)
        {
            // Return 200 OK with a JSON confirming successful validation and echoing the input dates
            return Ok(new { Message = "Input validated successfully.", StartDate, EndDate });
        }

        // This endpoint demonstrates Standardized API Response Wrapping
        // Uses the APIResponseWrapperFilter attribute directly for automatic response formatting
        [HttpGet("standardized-response")]
        [APIResponseWrapperFilter]
        public IActionResult StandardizedResponse()
        {
            // Sample data object returned from the action method
            var data = new { Value = 123, Description = "Some data" };

            // Return 200 OK with the sample data; response filter will wrap this automatically
            return Ok(data);
        }

        // This endpoint demonstrates Execution Time Logging filter usage
        // ExecutionTimeLoggingFilter applied via TypeFilter attribute to measure execution duration
        [HttpGet("log-execution-time")]
        [TypeFilter(typeof(ExecutionTimeLoggingFilter))]
        public IActionResult LogExecutionTime()
        {
            // Simulate processing delay of 500 milliseconds to mimic some workload
            Thread.Sleep(500);

            // Return 200 OK with a simple confirmation message
            return Ok(new { Message = "Execution time logged." });
        }
    }
}
