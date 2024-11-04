using Services.Exceptions;
using Shared.Helpers;        // ApiResponse
using System.Net;

namespace MyProject.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)      // Called for each HTTP request
        {
            try
            {
                await _next(context);           // Pass context to the next middleware in the pipeline

                // Handle successful responses
                if (context.Response.StatusCode == (int)HttpStatusCode.OK && !context.Response.HasStarted)
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new ApiResponse<object> { Success = true, Message = "Request processed successfully." });
                }
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new ApiResponse<object> { Success = false, Message = ex.Message });
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new ApiResponse<object> { Success = false, Message = ex.Message });
            }
            catch (UnauthorizedAccessException)                 // Using built-in exception
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new ApiResponse<object> { Success = false, Message = "Unauthorized access." });
            }
            catch (Exception ex)                    // For unhandled exceptions
            {
                Console.WriteLine($"Unhandled Exception: {ex}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ApiResponse<object> { Success = false, Message = "An error occurred while processing your request." });
            }
        }
    }
}
