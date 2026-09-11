

using MediatR;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Queries.GetStudentProfile;

public sealed record GetStudentProfileQuery(Guid userId) : IRequest<GetStudentProfileResponse>;
