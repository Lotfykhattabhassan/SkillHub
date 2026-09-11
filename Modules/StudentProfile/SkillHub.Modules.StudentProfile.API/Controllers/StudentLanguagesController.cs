using MediatR;
using Microsoft.AspNetCore.Mvc;
using SkillHub.Modules.StudentProfile.Application.Features.Languages.Commands.AddLanguage;
using SkillHub.Modules.StudentProfile.Application.Features.Languages.Commands.ChangeLanguageProficiency;
using SkillHub.Modules.StudentProfile.Application.Features.Languages.Commands.RemoveLanguage;
using SkillHub.Modules.StudentProfile.Domain.Enums;

namespace SkillHub.Modules.StudentProfile.API.Controllers
{
    [ApiController]
    [Route("api/student-languages")]
    public class StudentLanguagesController : ControllerBase
    {
        private readonly ISender _sender;

        public StudentLanguagesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("me")]
        public async Task<IActionResult> AddLanguage(
            [FromBody] AddLanguageCommand request,
            CancellationToken cancellationToken)
        {
            var response = await _sender.Send(
                request,
                cancellationToken);

            return Created(
                $"/api/student-languages/me/{response.Id}",
                response);
        }

        [HttpDelete("me/{languageId:int}")]
        public async Task<IActionResult> RemoveLanguage(
            int languageId,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new RemoveLanguageCommand(languageId),
                cancellationToken);

            return NoContent();
        }

        [HttpPatch("me/{languageId:int}/proficiency")]
        public async Task<IActionResult> ChangeLanguageProficiency(
            int languageId,
            [FromBody] ChangeLanguageProficiencyRequest request,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new ChangeLanguageProficiencyCommand(
                    languageId,
                    request.ProficiencyLevel),
                cancellationToken);

            return NoContent();
        }
    }

    public sealed record ChangeLanguageProficiencyRequest(
        LanguageProficiencyLevel ProficiencyLevel);
}
