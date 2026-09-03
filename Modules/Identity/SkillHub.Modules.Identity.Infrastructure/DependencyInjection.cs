using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkillHub.Modules.Identity.Application.Abstractions.Persistence;
using SkillHub.Modules.Identity.Application.Abstractions.Security;
using SkillHub.Modules.Identity.Infrastructure.Configuration;
using SkillHub.Modules.Identity.Infrastructure.Persistence;
using SkillHub.Modules.Identity.Infrastructure.Persistence.Repositories;
using SkillHub.Modules.Identity.Infrastructure.Security;

namespace SkillHub.Modules.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SkillHub");

        services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(connectionString));
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        services.AddHttpContextAccessor();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserCredentialRepository, UserCredentialRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        return services;
    }
}
