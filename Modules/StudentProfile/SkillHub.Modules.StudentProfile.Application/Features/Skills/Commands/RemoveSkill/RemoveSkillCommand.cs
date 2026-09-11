

using MediatR;

namespace SkillHub.Modules.StudentProfile.Application.Features.Skills.Commands.RemoveSkill;

public sealed record RemoveSkillCommand(int id) : IRequest<RemoveSkillResponse>;

