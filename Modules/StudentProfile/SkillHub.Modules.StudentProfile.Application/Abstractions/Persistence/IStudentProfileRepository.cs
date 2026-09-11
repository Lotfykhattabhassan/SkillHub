using System;
using SkillHub.Modules.StudentProfile;

namespace SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence
{
    public interface IStudentProfileRepository
    {
        Task<Domain.Entities.StudentProfile?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Domain.Entities.StudentProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<bool> ExistsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task AddAsync(Domain.Entities.StudentProfile studentProfile, CancellationToken cancellationToken = default);
       
    }
}
