using FoodDelivery.Application.Common.Interfaces.Authentication;
using FoodDelivery.Application.Common.Interfaces.Authentication.Services;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Infrastructure.Authentication;
using FoodDelivery.Infrastructure.Authentication.Services;
using FoodDelivery.Infrastructure.Persistence;
using FoodDelivery.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FoodDelivery.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddAuth(configuration).AddPersistence(configuration);
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            return services;
        }
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddDbContext<FoodDeliveryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            return services;
        }
        private static IServiceCollection AddAuth(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(
                               jwtSettings.SecretKey
                            )),
                        ClockSkew = TimeSpan.Zero

                    };
                    
                });
            services.AddAuthorization();
            return services;
        }


    }
}
