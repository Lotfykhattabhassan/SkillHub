using System;
using MediatR;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.CreateStudentProfile;

public sealed record CreateStudentProfileCommand(
    string fullName,
    string bio,
    DateTime dateOfBirth,
    string profileImageUrl,
    string university,
    string faculty,
    string department,
    string academicYear,
    float gpa
    ) : IRequest<CreateStudentProfileResponse>;

