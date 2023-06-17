using Infrastructure.Persistence.Configurations;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Extensions;

/// <summary>
/// Методы-расширения аутентификации
/// </summary>
public static class AuthenticationExtensions
{
    /// <summary>
    /// Добавляет JWT аутентификацию
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var authenticationConfig = new AuthenticationConfiguration();
        configuration.GetSection(AuthenticationConfiguration.SectionKey).Bind(authenticationConfig);

        services.AddSingleton(authenticationConfig);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = authenticationConfig.SymmetricSecurityKey(),

                ClockSkew = TimeSpan.Zero,
            };
        });
        return services;
    }
}