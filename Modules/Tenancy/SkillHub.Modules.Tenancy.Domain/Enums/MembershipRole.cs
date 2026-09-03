using System;
using System.Collections.Generic;
using System.Text;

namespace SkillHub.Modules.Tenancy.Domain.Enums;

[Flags]
public enum MembershipRole
{
    Owner = 1,
    Admin = 2,
    Manager = 4,
    HR = 8,
    Member = 16
}
