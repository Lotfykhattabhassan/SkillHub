

using FluentValidation;

namespace SkillHub.Modules.StudentProfile.Application.Features.Skills.Commands.ChangeSkillProficiency;

public class ChangeSkillProficiencyValidator : AbstractValidator<ChangeSkillProficiencyCommand>
{
    public ChangeSkillProficiencyValidator()
    {

        RuleFor(x => x.proficiencyLevel)
            .IsInEnum()
            .WithMessage("Invalid proficiency level.");
    }
}
