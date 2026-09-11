
using MediatR;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Application.Features.Exceptions;
using SkillHub.Modules.StudentProfile.Application.Features.Languages.Commands.AddLanguage;
using SkillHub.Modules.StudentProfile.Domain.Entities;

namespace SkillHub.Modules.StudentProfile.Application.Features.SocialLinks.Commands.AddSocialLink;

public class AddSocialLinkCommandHandler : IRequestHandler<AddSocialLinkCommand,AddSocialLinkResponse>
{
    private readonly IStudentSocialLinkRepository _studentSocialLinkRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStudentProfileRepository _studentProfileRepository;
    private readonly ICurrentUserStudentProfile _currentUserStudentProfile;
    public AddSocialLinkCommandHandler(
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

    public async Task<AddSocialLinkResponse> Handle(AddSocialLinkCommand request, CancellationToken cancellationToken)
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
        var exists = await _studentSocialLinkRepository.ExistsAsync(
            profile.Id,
            request.platform,
            cancellationToken);
        if (exists) {
            throw new DublicateLinkException();
        }
        
        var socialLink = StudentSocialLink.Create(profile.Id,
            request.platform,
            request.url);

        await _studentSocialLinkRepository.AddAsync(socialLink, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new AddSocialLinkResponse
        {
            Id = socialLink.Id
        };
    }


}
