using System;
using System.Collections.Generic;
using System.Text;

namespace SkillHub.Modules.Identity.Application.Abstractions.Security;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string passwordHashed);
}
