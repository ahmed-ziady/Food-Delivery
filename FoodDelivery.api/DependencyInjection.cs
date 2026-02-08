using FoodDelivery.api.Common.Mapping;
using FoodDelivery.Api.Extensions;
using Microsoft.OpenApi.Models;

namespace FoodDelivery.api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddMappings();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerDocumentation(); 
           

            return services;
        }
    }
}
