
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;

namespace SkillHub.Modules.Tenancy.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly TenancyDbContext _dbContext;
    public UnitOfWork(TenancyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
