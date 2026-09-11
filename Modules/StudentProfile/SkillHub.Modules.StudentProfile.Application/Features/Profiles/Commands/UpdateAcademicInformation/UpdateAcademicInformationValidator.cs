using FluentValidation;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.UpdateAcademicInformation;

public class UpdateAcademicInformationValidator : AbstractValidator<UpdateAcademicInformationCommand>
{
    public UpdateAcademicInformationValidator()
    {
        RuleFor(x => x.university)
            .NotEmpty()
            .WithMessage("University is required.")
            .MaximumLength(150).WithMessage("University cannot exceed 150 characters."); 
        
        RuleFor(x => x.faculty)
            .NotEmpty()
            .WithMessage("Faculty is required.")
            .MaximumLength(150).WithMessage("Faculty cannot exceed 150 characters."); 
        
        RuleFor(x => x.department)
            .NotEmpty()
            .WithMessage("Department is required.")
            .MaximumLength(150)
            .WithMessage("Department cannot exceed 150 characters."); 
        
        RuleFor(x => x.academicYear)
            .NotEmpty()
            .WithMessage("Academic year is required.")
            .MaximumLength(50)
            .WithMessage("Academic year cannot exceed 50 characters."); 
        
        RuleFor(x => x.gpa)
            .InclusiveBetween(0, 4)
            .WithMessage("GPA must be between 0 and 4.");
    }
}
