using HRM.Applicatin.Common.Exceptions;
using HRM.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace HRM.API.Common.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = context.Response;
            var result = new ErrorResponse();

            switch (exception)
            {
                case NotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    result.Message = exception.Message;
                    break;

                case UnauthorizedAccessException:
                case UnauthorizedException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    result.Message = "Unauthorized";
                    break;

                case AlreadyExist:
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    result.Message = exception.Message;
                    break;

                case AppException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    result.Message = exception.Message;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result.Message = "An unexpected error occurred.";
                    break;
            }

            var json = JsonSerializer.Serialize(result);
            return context.Response.WriteAsync(json);
        }

        private class ErrorResponse
        {
            public string Message { get; set; } = "Error";
        }
    }
}
