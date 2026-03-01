using System.Diagnostics;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Application.Common.Exceptions;

namespace FoodDelivery.Api.Middleware;

public sealed class ErrorHandlingMiddleware(
    RequestDelegate next,
    ILogger<ErrorHandlingMiddleware> logger,
    IWebHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
        {
            context.Response.StatusCode = 499;
        }
        catch (Exception ex)
        {
            if (context.Response.HasStarted)
            {
                logger.LogError(ex, "Response already started. TraceId: {TraceId}", context.TraceIdentifier);
                throw;
            }

            await WriteProblemDetailsAsync(context, ex);
        }
    }

    private async Task WriteProblemDetailsAsync(HttpContext context, Exception ex)
    {
        var traceId = Activity.Current?.Id ?? context.TraceIdentifier;

        var (status, title, type, code, errors, retryAfterSeconds) = Map(ex);

        logger.LogError(ex, "Exception mapped to {Status}. Code={Code}. TraceId={TraceId}", status, code, traceId);

        context.Response.Clear();
        context.Response.StatusCode = status;
        context.Response.ContentType = "application/problem+json";

        if (retryAfterSeconds is not null)
            context.Response.Headers.RetryAfter = retryAfterSeconds.Value.ToString();

        if (ex is ValidationException fv)
        {
            var dict = fv.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray());

            var vpd = new ValidationProblemDetails(dict)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation failed",
                Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1",
                Instance = context.Request.Path,
                Detail = env.IsDevelopment() ? "One or more validation errors occurred." : null
            };

            vpd.Extensions["traceId"] = traceId;
            vpd.Extensions["code"] = "VALIDATION_ERROR";

            await context.Response.WriteAsJsonAsync(vpd);
            return;
        }

        var pd = new ProblemDetails
        {
            Status = status,
            Title = title,
            Type = type,
            Instance = context.Request.Path,
            Detail = env.IsDevelopment() ? ex.Message : "An error occurred while processing your request."
        };

        pd.Extensions["traceId"] = traceId;

        if (!string.IsNullOrWhiteSpace(code))
            pd.Extensions["code"] = code;

        if (errors is not null)
            pd.Extensions["errors"] = errors;

        if (env.IsDevelopment())
            pd.Extensions["exception"] = ex.GetType().FullName;

        await context.Response.WriteAsJsonAsync(pd);
    }

    private static (int Status, string Title, string Type, string? Code, object? Errors, int? RetryAfterSeconds) Map(Exception ex)
    {
        return ex switch
        {
            BadRequestException e => (400, "Bad Request", Rfc(400), e.Code, null, null),
            UnauthorizedException e => (401, "Unauthorized", Rfc(401), e.Code, null, null),
            ForbiddenException e => (403, "Forbidden", Rfc(403), e.Code, null, null),
            NotFoundException e => (404, "Not Found", Rfc(404), e.Code, null, null),
            ConflictException e => (409, "Conflict", Rfc(409), e.Code, null, null),
            BusinessRuleException e => (422, "Unprocessable Entity", Rfc(422), e.Code, null, null),

            TooManyRequestsException e => (
                429,
                "Too Many Requests",
                Rfc(429),
                e.Code,
                null,
                e.RetryAfter is null ? null : (int)Math.Ceiling(e.RetryAfter.Value.TotalSeconds)
            ),

            ArgumentException => (400, "Bad Request", Rfc(400), "ARGUMENT_ERROR", null, null),
            KeyNotFoundException => (404, "Not Found", Rfc(404), "NOT_FOUND", null, null),
            UnauthorizedAccessException => (401, "Unauthorized", Rfc(401), "UNAUTHORIZED", null, null),
            InvalidOperationException => (409, "Conflict", Rfc(409), "CONFLICT", null, null),

            _ => (500, "Internal Server Error", Rfc(500), "SERVER_ERROR", null, null)
        };
    }

    private static string Rfc(int status) => status switch
    {
        400 => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1",
        401 => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.2",
        403 => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.4",
        404 => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.5",
        409 => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.10",
        422 => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.21",
        429 => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.29",
        _ => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.6.1"
    };
}
