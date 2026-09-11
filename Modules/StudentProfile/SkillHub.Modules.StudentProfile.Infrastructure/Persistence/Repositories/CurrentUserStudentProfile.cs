using Microsoft.AspNetCore.Http;

using SkillHub.Modules.StudentProfile.Application.Abstractions.Persistence;
using System.IdentityModel.Tokens.Jwt;

namespace SkillHub.Modules.StudentProfile.Infrastructure.Persistence.Repositories;

public class CurrentUserStudentProfile : ICurrentUserStudentProfile
{
    private readonly IHttpContextAccessor _httpContextAccesor;
    public CurrentUserStudentProfile(IHttpContextAccessor httpContextAccesor)
    {
        _httpContextAccesor = httpContextAccesor;
    }
    public Guid? Id
    {
        get
        {
            var value = _httpContextAccesor
                .HttpContext?
                .User
                .FindFirst(JwtRegisteredClaimNames.Sub)?
                .Value;

            return Guid.TryParse(value, out var userId)
                ? userId
                : null;
        }
    }
}
