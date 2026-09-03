using System;
using System.Collections.Generic;
using System.Text;
using SkillHub.Modules.Identity.Application.Abstractions.Persistence;

namespace SkillHub.Modules.Identity.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly IdentityDbContext _context;
    public UnitOfWork(IdentityDbContext context)
    {
        _context = context;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
