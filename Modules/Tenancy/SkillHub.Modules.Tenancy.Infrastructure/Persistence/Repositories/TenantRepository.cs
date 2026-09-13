using Microsoft.EntityFrameworkCore;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Domain.Entities;

namespace SkillHub.Modules.Tenancy.Infrastructure.Persistence.Repositories;

public class TenantRepository : ITenantRepository
{
    private readonly TenancyDbContext _dbContext;
    public TenantRepository(TenancyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Tenant> AddTenantAsync(Tenant tenant, CancellationToken cancellationToken = default)
    {
        await _dbContext.Tenants.AddAsync(tenant, cancellationToken);
        return tenant;
    }
    public void UpdateTenant(Tenant tenant)
    {
        _dbContext.Tenants.Update(tenant);
    }
    public async Task<Tenant?> GetTenantAsync(int tenantId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Tenants.FirstOrDefaultAsync(t => t.Id == tenantId, cancellationToken);
    }
    
}
