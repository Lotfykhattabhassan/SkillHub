using System;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;

namespace SkillHub.Modules.StudentProfile.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly StudentProfileDbContext _context;
    public UnitOfWork(StudentProfileDbContext context)
    {
        _context = context;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
