

using FluentValidation;

namespace SkillHub.Modules.StudentProfile.Application.Features.Languages.Commands.ChangeLanguageProficiency;

public class ChangeLanguageProficiencyValidator : AbstractValidator<ChangeLanguageProficiencyCommand>
{
    public ChangeLanguageProficiencyValidator()
    {

        RuleFor(x => x.proficiencyLevel)
            .IsInEnum()
            .WithMessage("Invalid proficiency level.");
    }
}
