using MediatR;
using Microsoft.AspNetCore.Mvc;
using SkillHub.Modules.StudentProfile.Application.Features.Skills.Commands.AddSkill;
using SkillHub.Modules.StudentProfile.Application.Features.Skills.Commands.ChangeSkillProficiency;
using SkillHub.Modules.StudentProfile.Application.Features.Skills.Commands.RemoveSkill;
using SkillHub.Modules.StudentProfile.Domain.Enums;

namespace SkillHub.Modules.StudentProfile.API.Controllers
{
    [ApiController]
    [Route("api/student-skills")]
    public class StudentSkillsController : ControllerBase
    {
        private readonly ISender _sender;

        public StudentSkillsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("me")]
        public async Task<IActionResult> AddSkill(
            [FromBody] AddSkillCommand request,
            CancellationToken cancellationToken)
        {
            var response = await _sender.Send(
                request,
                cancellationToken);

            return Created(
                $"/api/student-skills/me/{response.Id}",
                response);
        }

        [HttpDelete("me/{skillId:int}")]
        public async Task<IActionResult> RemoveSkill(
            int skillId,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new RemoveSkillCommand(skillId),
                cancellationToken);

            return NoContent();
        }

        [HttpPatch("me/{skillId:int}/proficiency")]
        public async Task<IActionResult> ChangeSkillProficiency(
            int skillId,
            [FromBody] ChangeSkillProficiencyRequest request,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new ChangeSkillProficiencyCommand(
                    skillId,
                    request.ProficiencyLevel),
                cancellationToken);

            return NoContent();
        }
    }

    public sealed record ChangeSkillProficiencyRequest(
        ProficiencyLevel ProficiencyLevel);
}
