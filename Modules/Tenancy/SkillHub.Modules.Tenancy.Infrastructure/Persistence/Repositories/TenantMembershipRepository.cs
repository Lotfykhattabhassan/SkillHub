using Microsoft.EntityFrameworkCore;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Domain.Entities;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Infrastructure.Persistence.Repositories;

public class TenantMembershipRepository : ITenantMembershipRepository
{
    private readonly TenancyDbContext _dbContext;
    public TenantMembershipRepository(TenancyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TenantMembership> AddMemberAsync(TenantMembership membership, CancellationToken cancellationToken = default)
    {
        await _dbContext.Memberships.AddAsync(membership, cancellationToken);
        return membership;
    }

    public async Task<TenantMembership?> GetByIdAsync(int id, int tenantId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Memberships.FirstOrDefaultAsync(m => m.Id == id && m.TenantId == tenantId, cancellationToken);
    }

    public async Task<TenantMembership?> GetByUserAndTenantAsync(
        Guid userId,
        int tenantId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Memberships.FirstOrDefaultAsync(
            m => m.UserId == userId && m.TenantId == tenantId,
            cancellationToken);
    }

    public async Task<List<TenantMembership>> GetMembersAsync(int tenantId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Memberships
            .Where(m => m.TenantId == tenantId)
            .ToListAsync(cancellationToken);
    }

    public void RemoveMember(TenantMembership membership)
    {
        _dbContext.Memberships.Remove(membership);
    }

    public async Task<TenantMembership?> GetActiveMembershipAsync(
    Guid userId,
    int tenantId,
    CancellationToken cancellationToken = default)
    {
        return await _dbContext.Memberships
    .FirstOrDefaultAsync(
        x =>
            x.UserId == userId &&
            x.TenantId == tenantId &&
            x.Status == MembershipStatus.Active,
        cancellationToken);
    }

}
