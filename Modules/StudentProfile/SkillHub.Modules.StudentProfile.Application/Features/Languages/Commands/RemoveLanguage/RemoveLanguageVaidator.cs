

using FluentValidation;

namespace SkillHub.Modules.StudentProfile.Application.Features.Languages.Commands.RemoveLanguage;

public class RemoveLanguageVaidator : AbstractValidator<RemoveLanguageCommand>
{
    public RemoveLanguageVaidator()
    {
        RuleFor(x=>x.id)
            .NotEmpty()
            .WithMessage("Id should not be empty")
            .GreaterThan(0)
            .WithMessage("Id should be greate than 0");
    }
}
