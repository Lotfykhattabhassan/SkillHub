
using MediatR;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Application.Features.Exceptions;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.UpdateAcademicInformation;

public class UpdateAcademicInformationCommandHandler
    : IRequestHandler<UpdateAcademicInformationCommand,UpdateAcademicInformationResponse>
{
    private readonly IStudentProfileRepository _studentProfileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserStudentProfile _currentUserStudentProfile;
    public UpdateAcademicInformationCommandHandler(
        IStudentProfileRepository studentProfileRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserStudentProfile currentUserStudentProfile
        )
    {
        _studentProfileRepository = studentProfileRepository;
        _unitOfWork = unitOfWork;
        _currentUserStudentProfile = currentUserStudentProfile;
    }
    public async Task<UpdateAcademicInformationResponse> Handle(
        UpdateAcademicInformationCommand request,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserStudentProfile.Id;
        if (userId == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }
        var studentProfile = await _studentProfileRepository
            .GetByUserIdAsync(userId.Value, cancellationToken);
        if (studentProfile == null)
            throw new ProfilenotfoundException();

        studentProfile.ChangeAcademicInformation(
            request.university,
            request.faculty,
            request.department,
            request.academicYear,
            request.gpa);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateAcademicInformationResponse
        {
            Message = "Success"
        };
    }
}
