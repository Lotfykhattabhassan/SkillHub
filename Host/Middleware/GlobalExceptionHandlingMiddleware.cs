using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SkillHub.Modules.Identity.Application.Exceptions;

namespace SkillHub.Host.Middleware;

public sealed class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        var statusCode = exception switch
        {
            ValidationException => StatusCodes.Status400BadRequest,

            UserNotFoundException =>
                StatusCodes.Status404NotFound,

            InvalidCredentialsException =>
                StatusCodes.Status401Unauthorized,

            _ => StatusCodes.Status500InternalServerError
        };

        var detail = exception switch
        {
            ValidationException => exception.Message,

            UserNotFoundException => exception.Message,

            InvalidCredentialsException => exception.Message,

            _ => "An unexpected error occurred."
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = GetTitle(statusCode),
            Detail = detail
        };

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    private static string GetTitle(int statusCode)
    {
        return statusCode switch
        {
            400 => "Validation Error",
            401 => "Unauthorized",
            404 => "Not Found",
            _ => "Internal Server Error"
        };
    }
}
