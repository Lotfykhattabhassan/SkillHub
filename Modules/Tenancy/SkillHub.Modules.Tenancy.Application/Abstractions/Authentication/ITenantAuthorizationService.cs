using SkillHub.Modules.Tenancy.Domain.Entities;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;

public interface ITenantAuthorizationService
{
    Task<TenantMembership> GetCurrentMembershipAsync(CancellationToken cancellationToken = default);
    Task EnsureAnyRoleAsync(CancellationToken cancellationToken = default, params MembershipRole[] roles);
}
