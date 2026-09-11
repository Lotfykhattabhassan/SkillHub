

using MediatR;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Application.Features.Exceptions;
using SkillHub.Modules.StudentProfile.Application.Features.SocialLinks.Commands.AddSocialLink;

namespace SkillHub.Modules.StudentProfile.Application.Features.SocialLinks.Commands.ChangeSocialLinkUrl;

public class ChangeSocialLinkUrlCommandHandler : IRequestHandler<ChangeSocialLinkUrlCommand,ChangeSocialLinkUrlResponse>
{
    private readonly IStudentSocialLinkRepository _studentSocialLinkRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStudentProfileRepository _studentProfileRepository;
    private readonly ICurrentUserStudentProfile _currentUserStudentProfile;
    public ChangeSocialLinkUrlCommandHandler(
        IStudentSocialLinkRepository studentSocialLinkRepository,
        IUnitOfWork unitOfWork,
        IStudentProfileRepository studentProfileRepository,
        ICurrentUserStudentProfile currentUserStudentProfile)
    {
        _unitOfWork = unitOfWork;
        _studentSocialLinkRepository = studentSocialLinkRepository;
        _studentProfileRepository = studentProfileRepository;
        _currentUserStudentProfile = currentUserStudentProfile;
    }

    public async Task<ChangeSocialLinkUrlResponse> Handle(ChangeSocialLinkUrlCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserStudentProfile.Id;
        if (userId == null)
        {
            throw new UnauthorizedAccessException("User is not authorized to perform this action.");
        }

        var profile = await _studentProfileRepository.GetByUserIdAsync(userId.Value, cancellationToken);
        if (profile == null)
        {
            throw new ProfilenotfoundException();
        }

        var socialLink = await _studentSocialLinkRepository.GetByIdAsync(request.socialLinkId, cancellationToken);
        if (socialLink == null) throw new SocialLinknotfoundException();

        socialLink.ChangeUrl(request.url);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ChangeSocialLinkUrlResponse
        {
            Message = "successfully Changed"
        };
    }
}
