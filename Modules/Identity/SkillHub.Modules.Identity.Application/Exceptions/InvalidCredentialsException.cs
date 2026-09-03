using System;
using System.Collections.Generic;
using System.Text;

namespace SkillHub.Modules.Identity.Application.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException() : base("Invalid email or password.")
    {
        
    }
}
