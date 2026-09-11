

using MediatR;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Application.Features.Exceptions;
using SkillHub.Modules.StudentProfile.Application.Features.SocialLinks.Commands.AddSocialLink;

namespace SkillHub.Modules.StudentProfile.Application.Features.SocialLinks.Commands.RemoveSocialLink;

public class RemoveSocialLinkCommandHandler : IRequestHandler<RemoveSocialLinkCommand,RemoveSocialLinkResponse>
{
    private readonly IStudentSocialLinkRepository _studentSocialLinkRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveSocialLinkCommandHandler(
        IStudentSocialLinkRepository studentSocialLinkRepository,
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _studentSocialLinkRepository = studentSocialLinkRepository;
    }

    public async Task<RemoveSocialLinkResponse> Handle(RemoveSocialLinkCommand request, CancellationToken cancellationToken)
    {
        var socialLink = await _studentSocialLinkRepository.GetByIdAsync(request.id, cancellationToken);
        if(socialLink == null) throw new SocialLinknotfoundException();

        socialLink.MarkAsDeleted();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new RemoveSocialLinkResponse
        {
            Message = "Successfully Removed"
        };
    }
}
