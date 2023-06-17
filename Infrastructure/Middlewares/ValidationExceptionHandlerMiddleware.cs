using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middlewares;

public class ValidationExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            var errorsMessages = exception.Errors.Select(error => error.ErrorMessage).ToList();
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorsMessages));
        }
    }
}

public static class ValidationExceptionHandlerMiddlewareDecorator
{
    public static IApplicationBuilder UseValidationErrorHandler(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<ValidationExceptionHandlerMiddleware>();
        return applicationBuilder;
    }
}