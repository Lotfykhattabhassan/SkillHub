using System;
using SkillHub.BuildingBlocks.Domain.Common;
using SkillHub.Modules.StudentProfile.Domain.Enums;
namespace SkillHub.Modules.StudentProfile.Domain.Entities;

public class StudentSocialLink : BaseEntity<int>
{
    public int StudentProfileId { get; private set; }
    public SocialLinkPlatform Platform { get; private set; }
    public string Url { get; private set; } = string.Empty;
    public StudentProfile StudentProfile { get; private set; } = null!;

    private StudentSocialLink() { }
    private StudentSocialLink(
        int studentProfileId,
        SocialLinkPlatform platform,
        string url
        )
    {
        if (studentProfileId <= 0)
            throw new ArgumentOutOfRangeException("StudentProfileId must be greater than 0", nameof(studentProfileId));
        StudentProfileId = studentProfileId;

        Platform = platform;

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp &&
                 uri.Scheme != Uri.UriSchemeHttps))
        {
            throw new ArgumentException(
                "Url must be a valid HTTP or HTTPS URL.",
                nameof(url));
        }
        Url = url;
    }

    public static StudentSocialLink Create(
        int studentProfileId,
        SocialLinkPlatform platform,
        string url
        )
    {
        return new StudentSocialLink(
            studentProfileId,
            platform,
            url);
    }

    public void ChangeUrl(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp &&
                 uri.Scheme != Uri.UriSchemeHttps))
        {
            throw new ArgumentException(
                "Url must be a valid HTTP or HTTPS URL.",
                nameof(url));
        }
        Url = url;
        MarkAsUpdated();
    }

}
