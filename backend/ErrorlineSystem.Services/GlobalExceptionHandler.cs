using Microsoft.AspNetCore.Http;

namespace ErrorlineSystem.Services
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;       

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;            
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {                
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    error = ex.Message,                
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
