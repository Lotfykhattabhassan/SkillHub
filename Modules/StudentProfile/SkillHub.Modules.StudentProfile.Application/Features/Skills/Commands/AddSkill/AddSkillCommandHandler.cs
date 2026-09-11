

using MediatR;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Application.Features.Exceptions;
using SkillHub.Modules.StudentProfile.Domain.Entities;

namespace SkillHub.Modules.StudentProfile.Application.Features.Skills.Commands.AddSkill;

public class AddSkillCommandHandler : IRequestHandler<AddSkillCommand,AddSkillResponse>
{
    private readonly IStudentSkillRepository _studentSkillRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStudentProfileRepository _studentProfileRepository;
    private readonly ICurrentUserStudentProfile _currentUserStudentProfile;
    public AddSkillCommandHandler(
        IStudentSkillRepository studentSkillRepository,
        IUnitOfWork unitOfWork,
        IStudentProfileRepository studentProfileRepository,
        ICurrentUserStudentProfile currentUserStudentProfile
        )
    {
        _studentSkillRepository = studentSkillRepository;
        _unitOfWork = unitOfWork;
        _studentProfileRepository = studentProfileRepository;
        _currentUserStudentProfile = currentUserStudentProfile;
    }
    public async Task<AddSkillResponse> Handle(AddSkillCommand request,CancellationToken cancellationToken)
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

        var exists = await _studentSkillRepository.ExistsAsync(
            profile.Id,
            request.skillName,
            cancellationToken);
        if (exists ) throw new DuplicateSkillException();

        var skill = StudentSkill.Create(profile.Id,
            request.skillName,
            request.proficiencyLevel);
        await _studentSkillRepository.AddAsync(skill, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new AddSkillResponse
        {
            Id = skill.Id
        };
    }
}
