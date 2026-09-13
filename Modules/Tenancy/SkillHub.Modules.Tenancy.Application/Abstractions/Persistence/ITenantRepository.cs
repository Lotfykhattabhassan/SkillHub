using SkillHub.Modules.Tenancy.Domain.Entities;

namespace SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;

public interface ITenantRepository
{
    Task<Tenant> AddTenantAsync(Tenant tenant,CancellationToken cancellationToken = default);
    void UpdateTenant(Tenant tenant);
    Task<Tenant?> GetTenantAsync(int tenantId,CancellationToken cancellationToken = default);
}
 
