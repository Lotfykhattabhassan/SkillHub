using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;
using SkillHub.Modules.Tenancy.Application.Exceptions;

namespace SkillHub.Modules.Tenancy.Infrastructure.Authentication;

public sealed class CurrentUserTenancy : ICurrentUserTenancy
{
    private const string TenantIdHeader = "X-Tenant-Id";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserTenancy(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;

    public Guid UserId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Guid.TryParse(value, out var userId)
                ? userId
                : throw new TenantContextMissingException();
        }
    }

    public string Email
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var value = user?.FindFirst(ClaimTypes.Email)?.Value
                        ?? user?.FindFirst("email")?.Value;

            if (string.IsNullOrWhiteSpace(value))
                throw new TenantContextMissingException();

            return value.Trim().ToLowerInvariant();
        }
    }

    public int TenantId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.Request
                .Headers[TenantIdHeader].ToString();

            return int.TryParse(value, out var tenantId) && tenantId > 0
                ? tenantId
                : throw new TenantContextMissingException();
        }
    }
}
