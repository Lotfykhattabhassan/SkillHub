

using FluentValidation;

namespace SkillHub.Modules.StudentProfile.Application.Features.Skills.Commands.RemoveSkill;

public class RemoveSkillValidator : AbstractValidator<RemoveSkillCommand>
{
    public RemoveSkillValidator()
    {
        RuleFor(x => x.id)
            .NotEmpty()
            .WithMessage("id cannot be empty")
            .GreaterThan(0)
            .WithMessage("id should be greater than 0");
    }
}
