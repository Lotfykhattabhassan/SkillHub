using System;
using System.Collections.Generic;
using System.Text;
using SkillHub.Modules.Identity.Domain.Enums;

namespace SkillHub.Modules.Identity.Application.Queries.GetUserById;

public class GetUserByIdResponse
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public UserType UserType { get; set; }
}
