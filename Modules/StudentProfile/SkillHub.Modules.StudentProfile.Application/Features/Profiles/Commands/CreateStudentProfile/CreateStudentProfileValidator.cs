using FluentValidation;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.CreateStudentProfile;

public sealed class CreateStudentProfileValidator
    : AbstractValidator<CreateStudentProfileCommand>
{
    public CreateStudentProfileValidator()
    {

        RuleFor(x => x.fullName)
            .NotEmpty()
            .MaximumLength(150)
            .WithMessage("Full name is required and must not exceed 150 characters.");

        RuleFor(x => x.bio)
            .MaximumLength(1000)
            .WithMessage("Bio is required and must not exceed 1000 characters.");

        RuleFor(x => x.dateOfBirth)
            .NotEmpty()
            .LessThan(DateTime.UtcNow)
            .WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.dateOfBirth)
            .Must(BeAtLeast18YearsOld)
            .WithMessage("Student must be at least 18 years old.");

        RuleFor(x => x.profileImageUrl)
            .NotEmpty()
            .MaximumLength(500)
            .WithMessage("Profile image URL is required and must not exceed 500 characters.");

        RuleFor(x => x.university)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("University is required and must not exceed 200 characters.");

        RuleFor(x => x.faculty)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Faculty is required and must not exceed 200 characters.");

        RuleFor(x => x.department)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Department is required and must not exceed 200 characters.");

        RuleFor(x => x.academicYear)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Academic year is required and must not exceed 50 characters.");

        RuleFor(x => x.gpa)
            .InclusiveBetween(0f, 4f)
            .WithMessage("GPA must be between 0 and 4.");
    }

    private static bool BeAtLeast18YearsOld(DateTime dateOfBirth)
    {
        var today = DateTime.UtcNow.Date;
        var birthDate = dateOfBirth.Date;

        var age = today.Year - birthDate.Year;

        if (birthDate > today.AddYears(-age))
            age--;

        return age >= 18;
    }
}
