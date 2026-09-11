
using MediatR;
using SkillHub.Modules.StudentProfile.Domain.Enums;

namespace SkillHub.Modules.StudentProfile.Application.Features.Skills.Commands.ChangeSkillProficiency;

public sealed record ChangeSkillProficiencyCommand(
    int skillId,
    ProficiencyLevel proficiencyLevel)
    : IRequest<ChangeSkillProficiencyResponse>
{
}
