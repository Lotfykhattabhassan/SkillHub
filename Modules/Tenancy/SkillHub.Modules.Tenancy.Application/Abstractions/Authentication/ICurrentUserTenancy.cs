namespace SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;

public interface ICurrentUserTenancy
{
    Guid UserId { get; }
    string Email { get; }
    int TenantId { get; }
}
