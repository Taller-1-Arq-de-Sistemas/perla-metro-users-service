using Microsoft.AspNetCore.Mvc;
using PerlaMetroUsersService.Exceptions;
using System.Security.Authentication;
using System.Text.Json;


namespace PerlaMetroUsersService.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger
        )
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
                if (exceptionMapping.TryGetValue(ex.GetType(), out var mapping))
                {
                    await GenerateHttpResponse(ex, context, mapping.ErrorMessage, mapping.StatusCode);
                }
                else
                {
                    await GenerateHttpResponse(ex, context, "Internal Server Error", 500);
                }
            }
        }

        private readonly Dictionary<Type, (string ErrorMessage, int StatusCode)> exceptionMapping = new()
        {
            { typeof(InvalidCredentialException), ("Invalid credentials", 401) },
            { typeof(UnauthorizedAccessException), ("Unauthorized access", 401) },
            { typeof(NotFoundException), ("Entity not found", 404) },
            { typeof(DuplicateException), ("Entity duplicated", 409) },
            { typeof(ConflictException), ("Conflict", 409) },
            { typeof(OperationCanceledException), ("Operation canceled", 499) },
            { typeof(InternalErrorException), ("Internal server error", 500) }
        };

        private async Task GenerateHttpResponse(
            Exception ex,
            HttpContext context,
            string errorTitle,
            int statusCode
        )
        {
            if (statusCode >= 500)
                _logger.LogError(ex, ex.Message);
            else
                _logger.LogWarning(ex, ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new ProblemDetails
            {
                Status = statusCode,
                Title = errorTitle,
                Detail = ex.Message,
                Instance = context.Request.Path, // Internal API URL that caused the error
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandler(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
