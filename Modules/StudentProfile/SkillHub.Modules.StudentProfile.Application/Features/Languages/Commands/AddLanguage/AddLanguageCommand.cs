

using MediatR;
using SkillHub.Modules.StudentProfile.Domain.Enums;

namespace SkillHub.Modules.StudentProfile.Application.Features.Languages.Commands.AddLanguage;

public sealed record AddLanguageCommand(
        string languageName,
        LanguageProficiencyLevel proficiencyLevel) : IRequest<AddLanguageResponse>;
