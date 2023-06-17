using System.Reflection;
using Application.Common.Behaviours;
using FluentValidation;
using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediator(options =>
        {
            options.Namespace = "Application";
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });
     
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return services;
    }
}