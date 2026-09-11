

using MediatR;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Queries.GetMyProfile;

public sealed record GetMyProfileQuery() : IRequest<GetMyProfileResponse>
{
}
