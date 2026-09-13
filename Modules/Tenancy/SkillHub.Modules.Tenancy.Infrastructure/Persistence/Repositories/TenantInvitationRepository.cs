
using Microsoft.EntityFrameworkCore;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Domain.Entities;

namespace SkillHub.Modules.Tenancy.Infrastructure.Persistence.Repositories;

public class TenantInvitationRepository : ITenantInvitationRepository
{
    private readonly TenancyDbContext _dbContext;
    public TenantInvitationRepository(TenancyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TenantInvitation> AddInvitationAsync(TenantInvitation invitation, CancellationToken cancellationToken = default)
    {
        await _dbContext.Invitations.AddAsync(invitation, cancellationToken);
        return invitation;
    }

    public async Task<TenantInvitation?> GetInvitationAsync(int id, int tenantId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Invitations.FirstOrDefaultAsync(i => i.Id == id && i.TenantId == tenantId, cancellationToken);
    }

    public async Task<TenantInvitation?> GetInvitationForAcceptanceAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Invitations
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

}
