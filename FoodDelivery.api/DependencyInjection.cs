using FoodDelivery.api.Common.Mapping;

namespace FoodDelivery.api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            // Add API services here (e.g., controllers, Swagger, etc.)
            services.AddMappings();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
    }
}
