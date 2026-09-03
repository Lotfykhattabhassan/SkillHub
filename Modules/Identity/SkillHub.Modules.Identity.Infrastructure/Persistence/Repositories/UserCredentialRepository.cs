using System;
using System.Collections.Generic;
using System.Text;
using SkillHub.Modules.Identity.Application.Abstractions.Persistence;
using SkillHub.Modules.Identity.Domain.Entities;

namespace SkillHub.Modules.Identity.Infrastructure.Persistence.Repositories;

public class UserCredentialRepository : IUserCredentialRepository
{
    private readonly IdentityDbContext _context;
    public UserCredentialRepository(IdentityDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(UserCredential userCredential, CancellationToken cancellationToken = default)
    {
        if (userCredential != null)
        {
            await _context.userCredentials.AddAsync(userCredential, cancellationToken);
        }
    }
}
