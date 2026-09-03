using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace SkillHub.Modules.Identity.Application.Commands.LoginUser
{
    public sealed record LoginUserCommand
        (string Email,
        string Password)
        : IRequest<LoginUserResponse>;

}
