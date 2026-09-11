
using SkillHub.Modules.StudentProfile.Domain.Entities;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Queries.GetMyProfile;

public class GetMyProfileResponse
{
    public string FullName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public int Age {  get; set; }
    public string ProfileImageUrl { get; set; } = string.Empty;
    public string University { get; set; } = string.Empty;
    public string Faculty { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string AcademicYear { get; set; } = string.Empty;
    public float GPA { get; set; }
    public ICollection<StudentSkill> Skills { get; set; } = new List<StudentSkill>();
    public ICollection<StudentLanguage> Languages { get; set; } = new List<StudentLanguage>();
    public ICollection<StudentSocialLink> SocialLinks { get; set; } = new List<StudentSocialLink>();
}
