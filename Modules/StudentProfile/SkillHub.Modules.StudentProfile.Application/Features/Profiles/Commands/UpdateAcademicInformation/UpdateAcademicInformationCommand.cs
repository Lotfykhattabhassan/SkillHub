

using MediatR;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.UpdateAcademicInformation;

public sealed record UpdateAcademicInformationCommand(
    string university,
    string faculty,
    string department,
    string academicYear,
    float gpa
    ) 
    : IRequest<UpdateAcademicInformationResponse>;

