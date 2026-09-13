using MediatR;
using SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Application.Exceptions;
using SkillHub.Modules.Tenancy.Domain.Enums;
using SkillHub.Modules.Tenancy.Domain.Entities;

namespace SkillHub.Modules.Tenancy.Application.Features.Invitations.Commands.CreateInvitation;

public sealed class CreateInvitationCommandHandler : IRequestHandler<CreateInvitationCommand, CreateInvitationResponse>
{
    private readonly ITenantInvitationRepository _tenantInvitationRepository;
    private readonly ITenantRepository _tenantRepository;
    private readonly ICurrentUserTenancy _currentUserTenancy;
    private readonly ITenantAuthorizationService _authorization;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInvitationCommandHandler(
        ITenantInvitationRepository tenantInvitationRepository,
        ITenantRepository tenantRepository,
        ICurrentUserTenancy currentUserTenancy,
        ITenantAuthorizationService authorization,
        IUnitOfWork unitOfWork)
    {
        _tenantInvitationRepository = tenantInvitationRepository;
        _tenantRepository = tenantRepository;
        _currentUserTenancy = currentUserTenancy;
        _authorization = authorization;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateInvitationResponse> Handle(
        CreateInvitationCommand request,
        CancellationToken cancellationToken)
    {
        await _authorization.EnsureAnyRoleAsync(cancellationToken, MembershipRole.Owner, MembershipRole.Admin);

        var tenant = await _tenantRepository.GetTenantAsync(
            _currentUserTenancy.TenantId,
            cancellationToken);

        if (tenant is null)
            throw new TenantNotFoundException();

        var invitation = TenantInvitation.Create(
            tenant.Id,
            request.Email,
            request.Role,
            request.ExpiresAt);

        await _tenantInvitationRepository.AddInvitationAsync(
            invitation,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateInvitationResponse
        {
            InvitationId = invitation.Id,
            TenantId = invitation.TenantId
        };
    }
}
