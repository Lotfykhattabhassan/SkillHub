using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Domain.Entities;
using SkillHub.Modules.StudentProfile.Infrastructure.Persistence;

namespace SkillHub.Modules.StudentProfile.Infrastructure.Persistence.Repositories;

public class StudentLanguageRepository : IStudentLanguageRepository
{
    private readonly StudentProfileDbContext _context;
    public StudentLanguageRepository(
        StudentProfileDbContext context
        )
    {
        _context = context;
    }

    public async Task<StudentLanguage?> GetByIdAsync(
        int id, 
        CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            throw new ArgumentException(nameof(id));
        var studentLanguage = await _context
            .StudentLanguages
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (studentLanguage == null)
            return null;
        return studentLanguage;
    }
    public async Task<IEnumerable<StudentLanguage>> GetByStudentProfileIdAsync(
        int studentProfileId, 
        CancellationToken cancellationToken = default)
    {
        if (studentProfileId <= 0)
            throw new ArgumentException(nameof(studentProfileId));

        var studentLanguages = await _context
            .StudentLanguages
            .Where(x => x.StudentProfileId == studentProfileId)
            .ToListAsync(cancellationToken);

        return studentLanguages;
    }
    public async Task<bool> ExistsAsync(
        int studentProfileId, 
        string languageName, 
        CancellationToken cancellationToken = default)
    {
        if (studentProfileId <= 0)
            throw new ArgumentException(nameof(studentProfileId));
        if(string.IsNullOrWhiteSpace(languageName))
            throw new ArgumentException(nameof(languageName));

        var exist = await _context
            .StudentLanguages
            .AnyAsync(x => x.StudentProfileId == studentProfileId &&
            x.LanguageName == languageName, cancellationToken
            );
        return exist;
    }
    public async Task AddAsync(
        StudentLanguage studentLanguage,
        CancellationToken cancellationToken = default)
    {
        if (studentLanguage == null)
            throw new ArgumentException(nameof(studentLanguage));
        await _context
            .StudentLanguages
            .AddAsync(studentLanguage, cancellationToken);

    }
}
