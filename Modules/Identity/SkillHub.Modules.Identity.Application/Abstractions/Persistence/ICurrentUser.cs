using System;
using System.Collections.Generic;
using System.Text;
using SkillHub.Modules.Identity.Domain.Entities;
using SkillHub.Modules.Identity.Domain.Enums;

namespace SkillHub.Modules.Identity.Application.Abstractions.Persistence;

public interface ICurrentUser
{
    public Guid? Id { get; }
    public string? Email { get; }
    public UserType? UserType { get; }
    public bool IsAuthenticated { get; }

}
