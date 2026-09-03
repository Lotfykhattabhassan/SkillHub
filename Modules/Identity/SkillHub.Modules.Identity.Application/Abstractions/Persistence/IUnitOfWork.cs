using System;
using System.Collections.Generic;
using System.Text;

namespace SkillHub.Modules.Identity.Application.Abstractions.Persistence;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}
