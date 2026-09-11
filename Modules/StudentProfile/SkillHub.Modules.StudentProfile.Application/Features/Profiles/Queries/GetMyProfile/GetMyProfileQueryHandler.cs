

using MediatR;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Application.Features.Exceptions;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Queries.GetMyProfile;

public class GetMyProfileQueryHandler : 
    IRequestHandler<GetMyProfileQuery,GetMyProfileResponse>
{
    private readonly IStudentProfileRepository _studentProfileRepository;
    private readonly ICurrentUserStudentProfile _currentUserStudentProfile;
    public GetMyProfileQueryHandler(
        IStudentProfileRepository studentProfileRepository,
        ICurrentUserStudentProfile currentUserStudentProfile)
    {
        _studentProfileRepository = studentProfileRepository;
        _currentUserStudentProfile = currentUserStudentProfile;
    }

    public async Task<GetMyProfileResponse> Handle(GetMyProfileQuery request,CancellationToken cancellationToken)
    {
        var userId = _currentUserStudentProfile.Id;
        if (userId == null) throw new UnauthorizedAccessException("User is not authenticated.");

        var studentProfile = await _studentProfileRepository.GetByUserIdAsync(userId.Value, cancellationToken);
        if(studentProfile == null) throw new ProfilenotfoundException();

        return new GetMyProfileResponse
        {
            FullName = studentProfile.FullName,
            Bio = studentProfile.Bio,
            Age = studentProfile.Age,
            ProfileImageUrl = studentProfile.ProfileImageUrl,
            University = studentProfile.University,
            Faculty = studentProfile.Faculty,
            Department = studentProfile.Department,
            AcademicYear = studentProfile.AcademicYear,
            GPA =studentProfile.GPA,
            Skills = studentProfile.Skills,
            Languages = studentProfile.Languages,
            SocialLinks = studentProfile.SocialLinks
        };
    }
}
