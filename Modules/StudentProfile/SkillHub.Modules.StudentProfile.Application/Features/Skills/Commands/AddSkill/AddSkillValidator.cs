
using FluentValidation;

namespace SkillHub.Modules.StudentProfile.Application.Features.Skills.Commands.AddSkill;

public class AddSkillValidator : AbstractValidator<AddSkillCommand>
{
    public AddSkillValidator()
    {
        
        RuleFor(x => x.skillName)
            .NotEmpty()
            .WithMessage("Skill name is required.")
            .MaximumLength(100)
            .WithMessage("Skill name cannot exceed 100 characters."); 
        
        RuleFor(x => x.proficiencyLevel)
            .IsInEnum()
            .WithMessage("Invalid proficiency level.");
    }
}
