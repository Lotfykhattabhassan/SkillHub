using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SkillHub.Modules.StudentProfile.Application.Behaviors;

namespace SkillHub.Modules.StudentProfile.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddStudentProfileApplication(
         this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);

            cfg.AddOpenBehavior(
                typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);
        return services;
    }
}

