using System;

using SkillHub.Modules.Identity.Domain.Entities;

namespace SkillHub.Modules.Identity.Application.Abstractions.Persistence;

public interface IUserCredentialRepository
{
    Task AddAsync(UserCredential userCredential,
        CancellationToken cancellationToken = default);
}
