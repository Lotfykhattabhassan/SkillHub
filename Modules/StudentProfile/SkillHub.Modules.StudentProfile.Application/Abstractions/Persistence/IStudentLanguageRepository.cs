using System;
using SkillHub.Modules.StudentProfile.Domain.Entities;

namespace SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;

public interface IStudentLanguageRepository
{
    Task<StudentLanguage?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<StudentLanguage>> GetByStudentProfileIdAsync(int studentProfileId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int studentProfileId, string languageName, CancellationToken cancellationToken = default);
    Task AddAsync(StudentLanguage studentLanguage, CancellationToken cancellationToken = default);
   
}
