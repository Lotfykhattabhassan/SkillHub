using System;
using System.Collections.Generic;
using System.Text;

namespace SkillHub.Modules.Tenancy.Domain.Enums;

public enum InvitationStatus
{
    Pending,
    Accepted,
    Rejected,
    Expired,
    Revoked
}
