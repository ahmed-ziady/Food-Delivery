using FoodDelivery.api.Common.Mapping;
using FoodDelivery.Api.Extensions;
using FoodDelivery.Application.Common;
using Microsoft.OpenApi.Models;

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
            return services;
        }
    }
}
