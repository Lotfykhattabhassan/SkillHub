using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Domain.Entities;
using SkillHub.Modules.StudentProfile.Infrastructure.Persistence;

namespace SkillHub.Modules.StudentProfile.Infrastructure.Persistence.Repositories;

public class StudentProfileRepository : IStudentProfileRepository
{
    private readonly StudentProfileDbContext _context;
    public StudentProfileRepository(
        StudentProfileDbContext context
        )
    {
        _context = context;
    }
    public async Task<Domain.Entities.StudentProfile?> GetByIdAsync(
        int id, 
        CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            throw new ArgumentException(nameof(id));

        var studentProfile = await _context
            .StudentProfiles
            .Include(x => x.Skills)
            .Include(x => x.SocialLinks)
            .Include(x => x.Languages)
            .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);

        if (studentProfile == null)
            return null;

        return studentProfile;

    }

    public async Task<Domain.Entities.StudentProfile?> GetByUserIdAsync(
        Guid userId, 
        CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException(nameof(userId));

        var studentProfile = await _context
           .StudentProfiles
           .Include(x => x.Skills)
           .Include(x => x.SocialLinks)
           .Include(x => x.Languages)
           .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (studentProfile == null)
            return null;

        return studentProfile;
    }
    public async Task<bool> ExistsByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException(nameof(userId));

        var exists = await _context
           .StudentProfiles
           .AnyAsync(s => s.UserId == userId, cancellationToken);
        return exists;
    }
    public async Task AddAsync(
        Domain.Entities.StudentProfile studentProfile,
        CancellationToken cancellationToken = default)
    {
        if (studentProfile == null)
            throw new ArgumentException(nameof(studentProfile));
        
        await _context.StudentProfiles.AddAsync(studentProfile, cancellationToken);

    }
   
   
}
