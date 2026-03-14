using FoodDelivery.api.Common.Mapping;
using FoodDelivery.Api.Extensions;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace FoodDelivery.api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddPresentationMappings();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerDocumentation();
            services.AddRateLimiter(options =>
            {
                options.AddPolicy("login-limit", httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 5,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0,
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                        }));

                options.AddPolicy("register-limit", httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 3,
                            Window = TimeSpan.FromMinutes(5),
                            QueueLimit = 0
                        }));

                options.AddPolicy("user-limit", httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User.FindFirst("sub")?.Value ?? "anonymous",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 20,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 2,
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                        }));

                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                    await context.HttpContext.Response.WriteAsJsonAsync(new
                    {
                        message = "Too many requests. Please try again later."
                    }, cancellationToken: token);
                };
            });

            return services;
        }
    }
}
