using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SkillHub.Modules.Identity.Application.Abstractions.Persistence;
using SkillHub.Modules.Identity.Domain.Entities;

namespace SkillHub.Modules.Identity.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _context;
    public UserRepository(IdentityDbContext context)
    {
        _context = context;
    }
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _context.users.
            Include(x => x.UserCredential).
            FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        if (user == null) return null;
        return user;
    }
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _context.users
            .Include(x => x.UserCredential)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (user == null) return null;
        return user;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        if ( user != null)
        {
            await _context.users.AddAsync(user, cancellationToken);
        }
    }


}
