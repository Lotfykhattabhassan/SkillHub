using MediatR;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.CreateStudentProfile;

public class CreateStudentProfileCommandHandler :
    IRequestHandler<CreateStudentProfileCommand, CreateStudentProfileResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStudentProfileRepository _studentProfileRepository;
    private readonly ICurrentUserStudentProfile _currentUserStudentProfile;
    public CreateStudentProfileCommandHandler(
        IUnitOfWork unitOfWork,
        IStudentProfileRepository studentProfileRepository,
        ICurrentUserStudentProfile currentUserStudentProfile)
    {
        _unitOfWork = unitOfWork;
        _studentProfileRepository = studentProfileRepository;
        _currentUserStudentProfile = currentUserStudentProfile;
    }

    public async Task<CreateStudentProfileResponse> Handle(
        CreateStudentProfileCommand request,
        CancellationToken cancellationToken)
    {
      var userId = _currentUserStudentProfile.Id;
        if (userId == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }
        var studentProfile = Domain.Entities.StudentProfile.Create(
            userId.Value,
            request.fullName,
            request.bio,
            request.dateOfBirth,
            request.profileImageUrl,
            request.university,
            request.faculty,
            request.department,
            request.academicYear,
            request.gpa);

        await _studentProfileRepository.AddAsync(
            studentProfile,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateStudentProfileResponse
        {
            Id = studentProfile.Id
        };
    }
}
