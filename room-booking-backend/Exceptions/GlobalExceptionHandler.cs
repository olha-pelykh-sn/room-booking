using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace room_booking_backend.Exeptions
{
    internal sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = ResolveStatusCode(exception);

            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Type   = exception.GetType().Name,
                    Title  = ResolveTitle(exception),
                    Detail = exception.Message,
                    Status = httpContext.Response.StatusCode
                }
            });
        }

        private static int ResolveStatusCode(Exception exception) => exception switch
        {
            KeyNotFoundException      => StatusCodes.Status404NotFound,
            InvalidOperationException => StatusCodes.Status409Conflict,
            ArgumentException         => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            _                         => StatusCodes.Status500InternalServerError
        };

        private static string ResolveTitle(Exception exception) => exception switch
        {
            KeyNotFoundException      => "Not Found",
            InvalidOperationException => "Conflict",
            ArgumentException         => "Bad Request",
            UnauthorizedAccessException => "Unauthorized",
            _                         => "Internal Server Error"
        };
    }
}
