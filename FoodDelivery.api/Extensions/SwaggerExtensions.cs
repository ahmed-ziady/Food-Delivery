using Microsoft.OpenApi.Models;

namespace FoodDelivery.Api.Extensions;

public static class SwaggerExtensions
{

    public static IServiceCollection AddSwaggerDocumentation( this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Food Delivery API",
                Version = "v1",
                Description = """
                    RESTful API built with ASP.NET Core (.NET 10).
                    Supports JWT Bearer Authentication.
                    """
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });

            options.EnableAnnotations();
        });

        return services;
    }
}
