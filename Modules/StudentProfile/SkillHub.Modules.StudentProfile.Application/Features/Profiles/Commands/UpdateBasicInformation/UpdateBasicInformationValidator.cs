using FluentValidation;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.UpdateBasicInformation;

public class UpdateBasicInformationValidator : AbstractValidator<UpdateBasicInformationCommand>
{
    public UpdateBasicInformationValidator()
    {
        RuleFor(x => x.fullName)
            .NotEmpty()
            .MaximumLength(150)
            .WithMessage("Full name is required and must not exceed 150 characters.");
       
        RuleFor(x => x.bio)
            .MaximumLength(1000)
            .WithMessage("Bio is required and must not exceed 1000 characters.");
    }
}
