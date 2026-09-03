using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SkillHub.Modules.Identity.Domain.Enums;

namespace SkillHub.Modules.Identity.Application.Commands.RegisterUser
{
    public sealed record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    DateTime DateOfBirth,
    UserType UserType,
    string Password
) : IRequest<Guid>;
}
