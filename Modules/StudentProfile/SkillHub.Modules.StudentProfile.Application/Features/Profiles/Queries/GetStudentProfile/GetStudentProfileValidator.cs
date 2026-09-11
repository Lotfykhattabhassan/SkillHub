using FluentValidation;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Queries.GetStudentProfile;

public class GetStudentProfileValidator : AbstractValidator<GetStudentProfileQuery>
{
    public GetStudentProfileValidator()
    {
        RuleFor(x=> x.userId)
            .NotEmpty().WithMessage("UserId is required.")
            .Must(userId => userId != Guid.Empty).WithMessage("UserId cannot be an empty GUID.");
    }
}
