using System;
using SkillHub.Modules.StudentProfile.Domain.Entities;
using SkillHub.Modules.StudentProfile.Domain.Enums;

namespace SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;

public interface IStudentSocialLinkRepository
{
    Task<StudentSocialLink?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<StudentSocialLink>> GetByStudentProfileIdAsync(int studentProfileId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int studentProfileId, SocialLinkPlatform platform, CancellationToken cancellationToken = default);
    Task AddAsync(StudentSocialLink studentSocialLink, CancellationToken cancellationToken = default);
   
}
