using Microsoft.AspNetCore.Http;
using SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Application.Exceptions;

namespace SkillHub.Modules.Tenancy.Infrastructure.Authentication;

public class TenantMembershipMiddleware
{
    private const string TenantIdHeader = "X-Tenant-Id";

    private readonly RequestDelegate _next;

    public TenantMembershipMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ICurrentUserTenancy currentUser,
        ITenantMembershipRepository membershipRepository)
    {
        if (context.User.Identity?.IsAuthenticated != true ||
            !context.Request.Headers.ContainsKey(TenantIdHeader))
        {
            await _next(context);
            return;
        }

        var membership = await membershipRepository.GetActiveMembershipAsync(
            currentUser.UserId,
            currentUser.TenantId,
            context.RequestAborted);

        if (membership is null)
            throw new TenantAccessDeniedException();

        await _next(context);
    }
}
