

using MediatR;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Application.Features.Exceptions;
using SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.UpdateAcademicInformation;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.ChangeProfileVisibility;

public class ChangeProfileVisibilityHandler 
    : IRequestHandler<ChangeProfileVisibilityCommand,ChangeProfileVisibilityResponse>
{
    private readonly IStudentProfileRepository _studentProfileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserStudentProfile _currentUserStudentProfile;
    public ChangeProfileVisibilityHandler(
        IStudentProfileRepository studentProfileRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserStudentProfile currentUserStudentProfile)
    {
        _unitOfWork = unitOfWork;
        _studentProfileRepository = studentProfileRepository;
        _currentUserStudentProfile = currentUserStudentProfile;
    }

    public async Task<ChangeProfileVisibilityResponse> Handle(
        ChangeProfileVisibilityCommand request,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserStudentProfile.Id;
        if (userId == null) throw new UnauthorizedAccessException("User is not authenticated.");
        var studentProfile = await _studentProfileRepository
           .GetByUserIdAsync(userId.Value, cancellationToken);
        if (studentProfile == null)
            throw new ProfilenotfoundException();

        studentProfile.SetVisibility(request.isPublic);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ChangeProfileVisibilityResponse
        {
            Message = "Success"
        };
    }
}
