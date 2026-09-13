using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Infrastructure.Authentication;
using SkillHub.Modules.Tenancy.Infrastructure.Persistence;
using SkillHub.Modules.Tenancy.Infrastructure.Persistence.Repositories;

namespace SkillHub.Modules.Tenancy.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddTenancyInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SkillHub");
        
        services.AddDbContext<TenancyDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITenantMembershipRepository, TenantMembershipRepository>();
        services.AddScoped<ITenantInvitationRepository, TenantInvitationRepository>();

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserTenancy, CurrentUserTenancy>();
        services.AddScoped<ITenantAuthorizationService, TenantAuthorizationService>();

        return services;
    }
}
