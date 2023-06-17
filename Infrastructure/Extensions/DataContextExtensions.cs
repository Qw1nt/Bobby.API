using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

/// <summary>
/// Методы-расширения для DataContext
/// </summary>
public static class DataContextExtensions
{
    /// <summary>
    /// Подключить PostgreSQL
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDataContext, ApplicationDataContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(ConnectionStringConfiguration.Database));
        });

        return services;
    }
}