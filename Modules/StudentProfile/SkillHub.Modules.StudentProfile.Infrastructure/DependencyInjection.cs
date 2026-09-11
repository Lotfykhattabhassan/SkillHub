using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Infrastructure.Persistence;
using SkillHub.Modules.StudentProfile.Infrastructure.Persistence.Repositories;


namespace SkillHub.Modules.StudentProfile.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddStudentProfileInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SkillHub");

        services.AddDbContext<StudentProfileDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IStudentProfileRepository,StudentProfileRepository>();
        services.AddScoped<IStudentLanguageRepository,StudentLanguageRepository>();
        services.AddScoped<IStudentSkillRepository,StudentSkillRepository>();
        services.AddScoped<IStudentSocialLinkRepository,StudentSocialLinkRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICurrentUserStudentProfile, CurrentUserStudentProfile>();
        return services;
    }
}
