

using MediatR;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Application.Features.Exceptions;
using SkillHub.Modules.StudentProfile.Application.Features.Skills.Commands.RemoveSkill;

namespace SkillHub.Modules.StudentProfile.Application.Features.Skills.Commands.ChangeSkillProficiency;

public class ChangeSkillProficiencyCommandHandler : IRequestHandler<ChangeSkillProficiencyCommand,ChangeSkillProficiencyResponse>
{
    private readonly IStudentSkillRepository _studentSkillRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStudentProfileRepository _studentProfileRepository;
    private readonly ICurrentUserStudentProfile _currentUserStudentProfile;
    public ChangeSkillProficiencyCommandHandler(
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

    public async Task<ChangeSkillProficiencyResponse> Handle(ChangeSkillProficiencyCommand request, CancellationToken cancellationToken)
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

        var skill = await _studentSkillRepository.GetByIdAsync(request.skillId, cancellationToken);
        if (skill == null)
            throw new SkillnotfoundException();

        skill.ChangeProficiency(request.proficiencyLevel);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ChangeSkillProficiencyResponse
        {
            Message = "Successfully Changed"
        };
    }
    }
