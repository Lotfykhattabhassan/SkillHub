using System;
using System.Collections.Generic;
using System.Text;
using SkillHub.Modules.Identity.Domain.Entities;

namespace SkillHub.Modules.Identity.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
}
