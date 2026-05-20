using Microsoft.AspNetCore.Diagnostics;
using RSR.DAL.DTOs.Response;

namespace RSR.PL
{
    public class GlobalExcpetionHandler : IExceptionHandler
    {
        public async  ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errorDetails = new ErrorHandling
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "server error",
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(errorDetails);
            return true;
        }
    }
}
