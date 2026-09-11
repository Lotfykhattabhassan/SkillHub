using System;
using System.Collections.Generic;
using System.Text;
using SkillHub.Modules.StudentProfile.Domain.Entities;
namespace SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;

public interface IStudentSkillRepository
{
    Task<StudentSkill?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<StudentSkill>> GetByStudentProfileIdAsync(int studentProfileId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int studentProfileId, string skillName, CancellationToken cancellationToken = default);
    Task AddAsync(StudentSkill studentSkill, CancellationToken cancellationToken = default);
    
}
