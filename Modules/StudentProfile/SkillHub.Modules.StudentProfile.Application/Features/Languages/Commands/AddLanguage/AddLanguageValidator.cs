

using FluentValidation;

namespace SkillHub.Modules.StudentProfile.Application.Features.Languages.Commands.AddLanguage;

public class AddLanguageValidator : AbstractValidator<AddLanguageCommand>
{
    public AddLanguageValidator()
    {
        
        RuleFor(x => x.languageName)
            .NotEmpty()
            .WithMessage("Language name is required.")
            .MaximumLength(100)
            .WithMessage("Language name cannot exceed 100 characters."); 
        
        RuleFor(x => x.proficiencyLevel)
            .IsInEnum()
            .WithMessage("Invalid language proficiency level.");
    }
}
