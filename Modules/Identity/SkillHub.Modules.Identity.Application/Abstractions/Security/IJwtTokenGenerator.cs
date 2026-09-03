using System;
using System.Collections.Generic;
using System.Text;
using SkillHub.Modules.Identity.Application.Commands.LoginUser;
using SkillHub.Modules.Identity.Domain.Enums;

namespace SkillHub.Modules.Identity.Application.Abstractions.Security;

public interface IJwtTokenGenerator
{
    JwtTokenResult GenerateToken(Guid userId, string email, UserType type);
}
