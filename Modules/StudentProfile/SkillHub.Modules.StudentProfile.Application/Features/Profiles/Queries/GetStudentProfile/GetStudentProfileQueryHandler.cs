
using MediatR;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Application.Features.Exceptions;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Queries.GetStudentProfile;

public class GetStudentProfileQueryHandler : 
    IRequestHandler<GetStudentProfileQuery,GetStudentProfileResponse>
{
    private readonly IStudentProfileRepository _studentProfileRepository;
    public GetStudentProfileQueryHandler(IStudentProfileRepository studentProfileRepository)
    {
        _studentProfileRepository = studentProfileRepository;
    }

    public async Task<GetStudentProfileResponse> Handle(GetStudentProfileQuery request, CancellationToken cancellationToken)
    {
        var studentProfile = await _studentProfileRepository.GetByUserIdAsync(request.userId, cancellationToken);
        if (studentProfile == null) throw new ProfilenotfoundException();

        return new GetStudentProfileResponse
        {
            FullName = studentProfile.FullName,
            Bio = studentProfile.Bio,
            Age = studentProfile.Age,
            ProfileImageUrl = studentProfile.ProfileImageUrl,
            University = studentProfile.University,
            Faculty = studentProfile.Faculty,
            Department = studentProfile.Department,
            AcademicYear = studentProfile.AcademicYear,
            GPA = studentProfile.GPA,
            Skills = studentProfile.Skills,
            Languages = studentProfile.Languages,
            SocialLinks = studentProfile.SocialLinks
        };
    }
}
