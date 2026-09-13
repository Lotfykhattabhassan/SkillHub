using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SkillHub.Modules.Tenancy.Application.Behaviors;

namespace SkillHub.Modules.Tenancy.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddTenancyApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(cfg =>
        { 
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));

            });

        services.AddValidatorsFromAssembly(assembly);
        return services;
    }
}
