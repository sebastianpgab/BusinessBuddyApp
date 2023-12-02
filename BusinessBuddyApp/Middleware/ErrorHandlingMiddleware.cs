using BusinessBuddyApp.Exceptions;
using System.Diagnostics;

namespace BusinessBuddyApp.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException badRequestEx)
            {
                _logger.LogError(badRequestEx.Message, badRequestEx);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Resource not found");
            }
            catch (NotFoundException notFoundEx)
            {
                _logger.LogError(notFoundEx.Message, notFoundEx);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Resource not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
