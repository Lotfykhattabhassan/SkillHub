
using FluentValidation;

namespace SkillHub.Modules.StudentProfile.Application.Features.SocialLinks.Commands.RemoveSocialLink;

public class RemoveSocialLinkValidator : AbstractValidator<RemoveSocialLinkCommand>
{
    public RemoveSocialLinkValidator()
    {
        RuleFor(x => x.id)
            .NotEmpty()
            .WithMessage("id cannot be empty")
            .GreaterThan(0)
            .WithMessage("id should be greater than 0");
    }
}
