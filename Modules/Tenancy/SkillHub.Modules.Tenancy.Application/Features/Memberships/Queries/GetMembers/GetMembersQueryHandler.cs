using MediatR;
using SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;

namespace SkillHub.Modules.Tenancy.Application.Features.Memberships.Queries.GetMembers;

public sealed class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, IReadOnlyList<GetMembersResponse>>
{
    private readonly ITenantMembershipRepository _membershipRepository;
    private readonly ICurrentUserTenancy _currentUserTenancy;

    public GetMembersQueryHandler(
        ITenantMembershipRepository membershipRepository,
        ICurrentUserTenancy currentUserTenancy)
    {
        _membershipRepository = membershipRepository;
        _currentUserTenancy = currentUserTenancy;
    }

    public async Task<IReadOnlyList<GetMembersResponse>> Handle(
        GetMembersQuery request,
        CancellationToken cancellationToken)
    {
        var members = await _membershipRepository.GetMembersAsync(
            _currentUserTenancy.TenantId,
            cancellationToken);

        return members
            .Select(m => new GetMembersResponse(
                m.Id,
                m.UserId,
                m.Role,
                m.Status,
                m.JoinedAt))
            .ToList();
    }
}
