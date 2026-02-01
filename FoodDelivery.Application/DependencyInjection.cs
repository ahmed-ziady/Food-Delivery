using FluentValidation;
using FoodDelivery.Application.Authentication.Commands.Register;
using FoodDelivery.Application.Common.Behaviors;
using FoodDelivery.Application.Services.Authentication.Commands;
using FoodDelivery.Application.Services.Authentication.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FoodDelivery.Application
{
    public static class DependencyInjection  
    {
        public static IServiceCollection AddApplication (this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(
                    typeof(RegisterCommandHandler).Assembly);
            });
            services.AddScoped(typeof(IPipelineBehavior<,>),typeof( ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(
               Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
