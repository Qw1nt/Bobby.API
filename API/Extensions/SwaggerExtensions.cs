﻿using System.Reflection;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

/// <summary>
/// Методы-расширения для Swagger'a
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Расширенный swagger с поддержкой авторизации
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerGenWithAuthentication(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.DescribeAllParametersInCamelCase();

            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name!;
            var filePath = Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.xml");
            options.IncludeXmlComments(filePath);
    
            options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }
}