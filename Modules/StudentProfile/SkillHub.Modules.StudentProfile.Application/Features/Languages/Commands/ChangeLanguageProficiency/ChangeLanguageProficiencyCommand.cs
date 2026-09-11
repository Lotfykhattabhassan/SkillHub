

using MediatR;
using SkillHub.Modules.StudentProfile.Domain.Enums;

namespace SkillHub.Modules.StudentProfile.Application.Features.Languages.Commands.ChangeLanguageProficiency;

public sealed record ChangeLanguageProficiencyCommand(
    int languageId,
    LanguageProficiencyLevel proficiencyLevel)
    : IRequest<ChangeLanguageProficiencyResponse>;

