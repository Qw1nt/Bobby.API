using Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Files.SaveService;

/// <summary>
/// 
/// </summary>
public static class FileServiceExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="webHostEnvironment"></param>
    /// <typeparam name="TInterface"></typeparam>
    /// <typeparam name="TImplementation"></typeparam>
    /// <returns></returns>
    public static IServiceCollection AddFileService<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface: class, IFileService
        where TImplementation : FileServiceBase, TInterface
    {
        services.AddScoped<TInterface, TImplementation>();
        return services;
    }
}