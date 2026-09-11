

using MediatR;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Application.Features.Exceptions;
using SkillHub.Modules.StudentProfile.Application.Features.Languages.Commands.AddLanguage;

namespace SkillHub.Modules.StudentProfile.Application.Features.Languages.Commands.RemoveLanguage;

public class RemoveLanguageCommandHandler : IRequestHandler<RemoveLanguageCommand,RemoveLanguageResponse>
{
    private readonly IStudentLanguageRepository _studentLanguageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveLanguageCommandHandler(
        IStudentLanguageRepository studentLanguageRepository,
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _studentLanguageRepository = studentLanguageRepository;
    }

    public async Task<RemoveLanguageResponse> Handle(RemoveLanguageCommand request, CancellationToken cancellationToken)
    {
        var language = await _studentLanguageRepository.GetByIdAsync(request.id,cancellationToken);
        if (language == null) throw new LanguagenotfoundException();

        language.MarkAsDeleted();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new RemoveLanguageResponse
        {
            Message = "Successfully Deleted"
        };
    }


}
