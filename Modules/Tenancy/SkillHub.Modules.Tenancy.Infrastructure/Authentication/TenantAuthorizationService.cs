using SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Application.Exceptions;
using SkillHub.Modules.Tenancy.Domain.Entities;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Infrastructure.Authentication;

public sealed class TenantAuthorizationService : ITenantAuthorizationService
{
    private readonly ICurrentUserTenancy _currentUserTenancy;
    private readonly ITenantMembershipRepository _membershipRepository;

    public TenantAuthorizationService(
        ICurrentUserTenancy currentUserTenancy,
        ITenantMembershipRepository membershipRepository)
    {
        _currentUserTenancy = currentUserTenancy;
        _membershipRepository = membershipRepository;
    }

    public async Task<TenantMembership> GetCurrentMembershipAsync(CancellationToken cancellationToken = default)
    {
        var membership = await _membershipRepository.GetActiveMembershipAsync(
            _currentUserTenancy.UserId,
            _currentUserTenancy.TenantId,
            cancellationToken);

        return membership ?? throw new TenantAccessDeniedException();
    }

    public async Task EnsureAnyRoleAsync(
        CancellationToken cancellationToken = default,
        params MembershipRole[] roles)
    {
        if (roles is null || roles.Length == 0)
            throw new ArgumentException("At least one role is required.", nameof(roles));

        var membership = await GetCurrentMembershipAsync(cancellationToken);
        if (!roles.Any(role => (membership.Role & role) == role))
            throw new ForbiddenTenantOperationException();
    }
}
