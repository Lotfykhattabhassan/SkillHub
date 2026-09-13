
using SkillHub.Modules.Tenancy.Domain.Entities;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;

public interface ITenantMembershipRepository
{
    Task<TenantMembership> AddMemberAsync(TenantMembership membership, CancellationToken cancellationToken = default);
    Task<TenantMembership?> GetByIdAsync(int id, int tenantId, CancellationToken cancellationToken = default);
    Task<TenantMembership?> GetByUserAndTenantAsync(Guid userId, int tenantId, CancellationToken cancellationToken = default);
    Task<List<TenantMembership>> GetMembersAsync(int tenantId, CancellationToken cancellationToken = default);
    void RemoveMember(TenantMembership membership);
    Task<TenantMembership?> GetActiveMembershipAsync(
    Guid userId,
    int tenantId,
    CancellationToken cancellationToken = default);
}
