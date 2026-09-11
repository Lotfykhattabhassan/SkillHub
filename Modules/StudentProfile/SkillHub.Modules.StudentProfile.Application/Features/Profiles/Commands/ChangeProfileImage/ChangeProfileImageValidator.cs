
using FluentValidation;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.ChangeProfileImage;

public class ChangeProfileImageValidator : AbstractValidator<ChangeProfileImageCommand>
{
    public ChangeProfileImageValidator()
    {

        RuleFor(x=>x.profilePhotoUrl)
            .NotEmpty()
            .WithMessage("profilePhotoUrl is required.")
            .MaximumLength(200).WithMessage("profilePhotoUrl cannot exceed 200 characters.");
    }
}
