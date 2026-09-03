using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace SkillHub.Modules.Identity.Application.Commands.ChangePassword;

public sealed record ChangePasswordCommand(string Password,
    string NewPassword) : IRequest<ChangePasswordResponse>;

