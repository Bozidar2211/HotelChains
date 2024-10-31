using Services.Exceptions;
using System.Net;
using System.Text.Json;             //serijalizacija
using HotelChainAPI.Helpers;        //Api Response

namespace MyProject.Middlewares

{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)      //Zove se za svaki http request
        {
            try
            {
                await _next(context);           //salje context sledecem middleware-u u pipeline-u
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
            catch (UnauthorizedAccessException)                 //Nemamo custom pa koristimo Built in exception
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new ApiResponse<object> { Success = false, Message = "Unauthorized access." });
            }

            catch (Exception ex)                    //U slucaju da nemamo spreman exception
            {
                Console.WriteLine($"Unhandled Exception: {ex}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ApiResponse<object> { Success = false, Message = "An error occurred while processing your request." });
            }
        }
    }
}
