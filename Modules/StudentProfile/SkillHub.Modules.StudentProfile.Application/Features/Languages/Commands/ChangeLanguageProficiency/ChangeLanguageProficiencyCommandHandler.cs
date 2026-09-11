

using MediatR;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Application.Features.Exceptions;
using SkillHub.Modules.StudentProfile.Application.Features.Languages.Commands.AddLanguage;

namespace SkillHub.Modules.StudentProfile.Application.Features.Languages.Commands.ChangeLanguageProficiency;

public class ChangeLanguageProficiencyCommandHandler 
    : IRequestHandler<ChangeLanguageProficiencyCommand,ChangeLanguageProficiencyResponse>
{
    private readonly IStudentLanguageRepository _studentLanguageRepository;
    private readonly IStudentProfileRepository _studentProfileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserStudentProfile _currentUserStudentProfile;

    public ChangeLanguageProficiencyCommandHandler(
        IStudentLanguageRepository studentLanguageRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserStudentProfile currentUserStudentProfile,
        IStudentProfileRepository studentProfileRepository
        )
    {
        _unitOfWork = unitOfWork;
        _studentLanguageRepository = studentLanguageRepository;
        _currentUserStudentProfile = currentUserStudentProfile;
        _studentProfileRepository = studentProfileRepository;
    }
    public async Task<ChangeLanguageProficiencyResponse> Handle(ChangeLanguageProficiencyCommand request, CancellationToken cancellationToken)
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


        var language = await _studentLanguageRepository.GetByIdAsync(request.languageId,cancellationToken);
        if (language == null) throw new LanguagenotfoundException();

        language.ChangeLanguageProficiency(request.proficiencyLevel);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new ChangeLanguageProficiencyResponse
        {
            Message = "Successfully Changed"
        };
    }


}
