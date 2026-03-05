using FoodDelivery.Application.Authentication.Authentication;
using FoodDelivery.Application.Common.Interfaces;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Common.Interfaces.Services;
using FoodDelivery.Application.Common.Interfaces.Twilio;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Infrastructure.Authentication.Services;
using FoodDelivery.Infrastructure.Authentication.Settings;
using FoodDelivery.Infrastructure.Persistence;
using FoodDelivery.Infrastructure.Persistence.Repositories;
using FoodDelivery.Infrastructure.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FoodDelivery.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
            .AddPersistence(configuration)
            .AddAuth(configuration).
            AddIdentity();
        services.AddScoped<IUserRepository, UserRepository>();  
        services.AddScoped<IMenuRepository , MenuRepository>();
        services.AddScoped<IIngredientRepository, IngredientRepository>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        //services.Configure<TwilioSettings>(configuration.GetSection(TwilioSettings.SectionName));   
        //services.AddScoped<ISmsService, TwilioSmsService>();
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));
        services.Configure<GoogleAuthSettings>(configuration.GetSection(GoogleAuthSettings.SectionName));
        services .Configure<FacebookAuthSettings>(configuration.GetSection(FacebookAuthSettings.SectionName));
        services.AddScoped<IFacebookAuthValidator, FacebookAuthValidator>();    
        services.AddScoped<IGoogleAuthValidator, GoogleAuthValidator>();
        services.AddHttpClient();
        services.AddTransient<IMailingService, EmailService>();
        services.AddScoped<IImageStorageService, LocalImageStorageService>();
        services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromMinutes(10));

        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddDbContext<FoodDeliveryDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));



        // Only keep real repositories
        services.AddScoped<IMenuRepository, MenuRepository>();

        return services;
    }
    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 6;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        })
        .AddEntityFrameworkStores<FoodDeliveryDbContext>()
        .AddDefaultTokenProviders();
        return services;
    }
    private static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IVerifyOtp, VerificationOtpService>();
        services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                        ClockSkew = TimeSpan.Zero
                    };
            });

        services.AddAuthorization();

        return services;
    }
}
