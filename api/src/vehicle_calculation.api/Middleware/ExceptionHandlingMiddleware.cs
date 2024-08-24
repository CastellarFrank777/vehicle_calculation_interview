using System.Net;
using System.Text.Json;
using vehicle_calculation.common.ErrorHandling;

namespace vehicle_calculation.api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogManager _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogManager logger)
        {
            _next = next;
            _logger = logger;
            _logger.SetLogType(typeof(ExceptionHandlingMiddleware));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var (reference, _) = _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, reference);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, string codeReference)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                context.Response.StatusCode,
                Message = $"Internal Server Error. Please try again or contact an administrator. Reference: {codeReference}"
            }));
        }
    }
}
