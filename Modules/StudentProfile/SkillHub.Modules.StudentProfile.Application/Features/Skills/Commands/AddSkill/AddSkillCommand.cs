
using MediatR;
using SkillHub.Modules.StudentProfile.Domain.Enums;

namespace SkillHub.Modules.StudentProfile.Application.Features.Skills.Commands.AddSkill;

public sealed record AddSkillCommand(
     string skillName,
     ProficiencyLevel proficiencyLevel
    ) : IRequest<AddSkillResponse>
{
}
