using System;
using System.Collections.Generic;
using System.Text;

namespace SkillHub.Modules.Identity.Application.Commands.LoginUser;

public sealed record JwtTokenResult(
    string AccessToken
    ,DateTime ExpiresAt);

