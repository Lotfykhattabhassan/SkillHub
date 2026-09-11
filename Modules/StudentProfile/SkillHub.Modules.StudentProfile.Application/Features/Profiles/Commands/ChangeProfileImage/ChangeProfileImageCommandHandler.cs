
using MediatR;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Application.Features.Exceptions;
using SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.UpdateAcademicInformation;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.ChangeProfileImage;

public class ChangeProfileImageCommandHandler :
    IRequestHandler<ChangeProfileImageCommand,ChangeProfileImageResponse>
{
    private readonly IStudentProfileRepository _studentProfileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserStudentProfile _currentUserStudentProfile;

    public ChangeProfileImageCommandHandler(
        IStudentProfileRepository studentProfileRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserStudentProfile currentUserStudentProfile
        )
    {
        _studentProfileRepository = studentProfileRepository;
        _unitOfWork = unitOfWork;
        _currentUserStudentProfile = currentUserStudentProfile;
    }

    public async Task<ChangeProfileImageResponse> Handle(ChangeProfileImageCommand request,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserStudentProfile.Id;
        if (userId == null) throw new UnauthorizedAccessException("User is not authenticated.");
        var studentProfile = await _studentProfileRepository
           .GetByUserIdAsync(userId.Value, cancellationToken);
        if (studentProfile == null)
            throw new ProfilenotfoundException();  

        studentProfile.ChangeImage(request.profilePhotoUrl);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new ChangeProfileImageResponse
        {
            Message = "Success"
        };

    }
}
