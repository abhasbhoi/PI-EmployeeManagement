using System.Net;

namespace EmployeeManagement.Middlewares
{
    public class GlobalExceptionMiddleWarer
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public GlobalExceptionMiddleWarer(RequestDelegate next, ILogger<GlobalExceptionMiddleWarer> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync("Internal server error");
        }
    }
}
