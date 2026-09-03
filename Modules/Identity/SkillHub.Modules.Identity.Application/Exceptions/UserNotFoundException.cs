using System;
using System.Collections.Generic;
using System.Text;

namespace SkillHub.Modules.Identity.Application.Exceptions;

public sealed class UserNotFoundException : Exception
{
    public UserNotFoundException()
        : base("User was not found.")
    {
    }
}
