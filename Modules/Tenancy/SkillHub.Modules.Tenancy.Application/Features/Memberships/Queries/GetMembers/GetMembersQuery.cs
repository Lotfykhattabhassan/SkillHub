using MediatR;

namespace SkillHub.Modules.Tenancy.Application.Features.Memberships.Queries.GetMembers;

public sealed record GetMembersQuery : IRequest<IReadOnlyList<GetMembersResponse>>;
