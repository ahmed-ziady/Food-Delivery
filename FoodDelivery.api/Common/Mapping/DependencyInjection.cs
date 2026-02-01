using FluentValidation;
using Mapster;
using MapsterMapper;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace FoodDelivery.api.Common.Mapping
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMappings(this IServiceCollection services) 
        {
        

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
           
            return services;
        }
        
          
    }
}
