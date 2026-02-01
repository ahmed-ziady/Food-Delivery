using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodDelivery.api.Filteres
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var statusCode = exception switch
            {
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
                Title = GetTitle(statusCode),
                Detail = exception.Message,
                Status = statusCode,
                Instance = context.HttpContext.Request.Path,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new ObjectResult(problemDetails)
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
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
