using MediatR;
using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using SkillHub.Modules.StudentProfile.Application.Features.Exceptions;
using SkillHub.Modules.StudentProfile.Domain.Entities;

namespace SkillHub.Modules.StudentProfile.Application.Features.Languages.Commands.AddLanguage;

public class AddLanguageCommandHandler : IRequestHandler<AddLanguageCommand,AddLanguageResponse>
{
    private readonly IStudentLanguageRepository _studentLanguageRepository;
    private readonly IStudentProfileRepository _studentProfileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserStudentProfile _currentUserStudentProfile;

    public AddLanguageCommandHandler(
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

    public async Task<AddLanguageResponse> Handle(AddLanguageCommand request,CancellationToken cancellationToken)
    {
        var userId = _currentUserStudentProfile.Id;
        if(userId == null)
        {
            throw new UnauthorizedAccessException("User is not authorized to perform this action.");
        }

        var profile = await _studentProfileRepository.GetByUserIdAsync(userId.Value,cancellationToken);
        if (profile == null)
        {
            throw new ProfilenotfoundException();
        }
        var exists = await _studentLanguageRepository.ExistsAsync(
            profile.Id,
            request.languageName,
            cancellationToken);
        if (exists) throw new DuplicateLanguageException();

        var language = StudentLanguage.Create(profile.Id,
            request.languageName,
            request.proficiencyLevel);

        await _studentLanguageRepository.AddAsync(language,cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new AddLanguageResponse
        {
            Id = language.Id
        };
    }
}
