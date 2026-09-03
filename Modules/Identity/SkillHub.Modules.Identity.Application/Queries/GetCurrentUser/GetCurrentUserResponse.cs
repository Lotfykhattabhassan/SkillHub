using System;
using System.Collections.Generic;
using System.Text;
using SkillHub.Modules.Identity.Domain.Enums;

namespace SkillHub.Modules.Identity.Application.Queries.GetCurrentUser;

public class GetCurrentUserResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public UserType UserType { get; set; }
}
