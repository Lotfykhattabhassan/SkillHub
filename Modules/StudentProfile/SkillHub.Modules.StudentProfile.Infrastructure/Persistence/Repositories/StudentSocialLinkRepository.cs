using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Domain.Entities;
using SkillHub.Modules.StudentProfile.Domain.Enums;
using SkillHub.Modules.StudentProfile.Infrastructure.Persistence;

namespace SkillHub.Modules.StudentProfile.Infrastructure.Persistence.Repositories;

public class StudentSocialLinkRepository : IStudentSocialLinkRepository
{
    private readonly StudentProfileDbContext _context;
    public StudentSocialLinkRepository(
        StudentProfileDbContext context
        )
    {
        _context = context;
    }
    public async Task<StudentSocialLink?> GetByIdAsync(
        int id, 
        CancellationToken cancellationToken = default)
    {
        if( id <= 0 )
            throw new ArgumentException(nameof(id));
        var studentSocialLink = await _context
            .StudentSocialLinks
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (studentSocialLink == null)
            return null;
        return studentSocialLink;
    }
    public async Task<IEnumerable<StudentSocialLink>> GetByStudentProfileIdAsync(
        int studentProfileId, CancellationToken cancellationToken = default)
    {
        if (studentProfileId <= 0)
            throw new ArgumentException(nameof(studentProfileId));
        var studentSocialLinks = await _context
            .StudentSocialLinks
            .Where(x => x.StudentProfileId == studentProfileId)
            .ToListAsync(cancellationToken);

        return studentSocialLinks;
    }
    public async Task<bool> ExistsAsync(
        int studentProfileId,
        SocialLinkPlatform platform, 
        CancellationToken cancellationToken = default)
    {
        if (studentProfileId <= 0)
            throw new ArgumentException(nameof(studentProfileId));
        var exist = await _context
            .StudentSocialLinks
            .AnyAsync(x => x.StudentProfileId == studentProfileId &&
            x.Platform == platform
            , cancellationToken);

        return exist;
    }
    public async Task AddAsync(
        StudentSocialLink studentSocialLink,
        CancellationToken cancellationToken = default)
    {
        if (studentSocialLink == null)
            throw new ArgumentException(nameof(studentSocialLink));
        await _context
            .StudentSocialLinks
            .AddAsync(studentSocialLink, cancellationToken);
    }


}
