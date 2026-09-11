using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Domain.Entities;
using SkillHub.Modules.StudentProfile.Infrastructure.Persistence;

namespace SkillHub.Modules.StudentProfile.Infrastructure.Persistence.Repositories;

public class StudentSkillRepository : IStudentSkillRepository
{
    private readonly StudentProfileDbContext _context;
    public StudentSkillRepository(
      StudentProfileDbContext context
        )
    {
        _context = context;
    }
    public async Task<StudentSkill?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {

        if (id <= 0)
            throw new ArgumentException(nameof(id));

        var studentSkill = await _context
            .StudentSkills
            .FirstOrDefaultAsync( s => s.Id == id, cancellationToken);

        if (studentSkill == null)
            return null;

        return studentSkill;
    }

    public async Task<IEnumerable<StudentSkill>> GetByStudentProfileIdAsync(
        int studentProfileId,
        CancellationToken cancellationToken = default)
    {
        if (studentProfileId <= 0)
            throw new ArgumentException(nameof(studentProfileId));
        var studentSkills = await _context
            .StudentSkills
            .Where(s => s.StudentProfileId == studentProfileId)
            .ToListAsync(cancellationToken);

        return studentSkills;
    }
    public async Task<bool> ExistsAsync(
        int studentProfileId,
        string skillName,
        CancellationToken cancellationToken = default)
    {
        if (studentProfileId <= 0)
            throw new ArgumentException(nameof(studentProfileId));
        if (string.IsNullOrWhiteSpace(skillName))
            throw new ArgumentException(nameof(skillName));
        var exists = await _context
            .StudentSkills
            .AnyAsync(s => s.StudentProfileId == studentProfileId && s.SkillName == skillName, cancellationToken);
        return exists;
    }

    public async Task AddAsync(
        StudentSkill studentSkill
        , CancellationToken cancellationToken = default)
    {
        if (studentSkill == null)
            throw new ArgumentNullException(nameof(studentSkill));
        await _context.StudentSkills.AddAsync(studentSkill, cancellationToken);

    }
}
