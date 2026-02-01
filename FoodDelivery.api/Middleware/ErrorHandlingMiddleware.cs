using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FoodDelivery.Api.Middleware
{
    public sealed class ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger,
        IWebHostEnvironment environment)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Unhandled exception. TraceId: {TraceId}",
                    context.TraceIdentifier);

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // ✅ 1) Map status codes (add FluentValidation)
            var statusCode = exception switch
            {
                FluentValidation.ValidationException => StatusCodes.Status400BadRequest,

                ArgumentException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                AccessViolationException => StatusCodes.Status403Forbidden,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                InvalidOperationException => StatusCodes.Status409Conflict,
                ApplicationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = GetTitle(statusCode),
                Detail = environment.IsDevelopment()
                    ? exception.Message
                    : "An error occurred while processing your request.",
                Instance = context.Request.Path,
                Type = $"https://httpstatuses.com/{statusCode}"
            };

            // ✅ 2) Include validation errors in response body (super useful)
            if (exception is FluentValidation.ValidationException fvEx)
            {
                var errors = fvEx.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                problemDetails.Extensions["errors"] = errors;
            }

            problemDetails.Extensions["traceId"] = context.TraceIdentifier;

            context.Response.Clear();
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }

        private static string GetTitle(int statusCode) =>
            statusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                409 => "Conflict",
                422 => "Unprocessable Entity",
                _ => "Internal Server Error"
            };
    }
}
