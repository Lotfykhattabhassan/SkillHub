
using SkillHub.Modules.Tenancy.Domain.Entities;

namespace SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;

public interface ITenantInvitationRepository
{
    Task<TenantInvitation> AddInvitationAsync(TenantInvitation invitation, CancellationToken cancellationToken = default);
    Task<TenantInvitation?> GetInvitationAsync(int id, int tenantId, CancellationToken cancellationToken = default);
    Task<TenantInvitation?> GetInvitationForAcceptanceAsync(int id, CancellationToken cancellationToken = default);

}
