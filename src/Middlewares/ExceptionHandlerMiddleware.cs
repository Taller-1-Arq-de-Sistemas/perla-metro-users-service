using Microsoft.AspNetCore.Mvc;
using PerlaMetroUsersService.Exceptions;
using System.Security.Authentication;
using System.Text.Json;
using System.Diagnostics;


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
                    await GenerateHttpResponse(ex, context, mapping.ErrorMessage, mapping.StatusCode, mapping.Code);
                }
                else
                {
                    await GenerateHttpResponse(ex, context, "Internal Server Error", 500, "internal_error");
                }
            }
        }

        private readonly Dictionary<Type, (string ErrorMessage, int StatusCode, string Code)> exceptionMapping = new()
        {
            { typeof(InvalidCredentialException), ("Invalid credentials", 401, "invalid_credentials") },
            { typeof(UnauthorizedAccessException), ("Unauthorized access", 401, "unauthorized") },
            { typeof(NotFoundException), ("Entity not found", 404, "not_found") },
            { typeof(DuplicateException), ("Entity duplicated", 409, "duplicate") },
            { typeof(ConflictException), ("Conflict", 409, "conflict") },
            { typeof(OperationCanceledException), ("Operation canceled", 499, "operation_canceled") },
            { typeof(InternalErrorException), ("Internal server error", 500, "internal_error") }
        };

        private async Task GenerateHttpResponse(
            Exception ex,
            HttpContext context,
            string errorTitle,
            int statusCode,
            string code
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
                Type = $"https://httpstatuses.io/{statusCode}"
            };
            response.Extensions["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier;
            response.Extensions["code"] = code;

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
