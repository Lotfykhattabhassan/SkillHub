

using MediatR;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Application.Features.Exceptions;

namespace SkillHub.Modules.StudentProfile.Application.Features.Skills.Commands.RemoveSkill;

public class RemoveSkillCommandHandler : IRequestHandler<RemoveSkillCommand,RemoveSkillResponse>
{
    private readonly IStudentSkillRepository _studentSkillRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveSkillCommandHandler(
        IStudentSkillRepository studentSkillRepository,
        IUnitOfWork unitOfWork
        )
    {
        _studentSkillRepository = studentSkillRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RemoveSkillResponse> Handle(RemoveSkillCommand request,CancellationToken cancellationToken)
    {
        var skill = await _studentSkillRepository.GetByIdAsync(request.id, cancellationToken);
        if (skill == null)
            throw new SkillnotfoundException();
        skill.MarkAsDeleted();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new RemoveSkillResponse
        {
            Message = "Successfully Deleted"
        };
    }
}
