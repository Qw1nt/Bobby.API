using System.Text;
using Application.Common.Interfaces;
using Infrastructure.Extensions;
using Infrastructure.Files;
using Infrastructure.Files.SaveService;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        services.AddDataContext(configuration);
        services.AddScoped<FileServiceTools>();
        
        services.AddScoped<JwtGenerationService>();
        services.AddJwtAuthentication(configuration);
        
        services.AddScoped<IHashSaltService, ComputeHashSaltService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ISymmetricEncryptor, SymmetricEncryptor>();
        
        return services;
    }
}