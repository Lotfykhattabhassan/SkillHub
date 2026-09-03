using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using SkillHub.Modules.Identity.Application.Abstractions.Persistence;
using SkillHub.Modules.Identity.Domain.Enums;

namespace SkillHub.Modules.Identity.Infrastructure.Persistence.Repositories;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccesor;
    public CurrentUser(IHttpContextAccessor httpContextAccesor)
    {
        _httpContextAccesor = httpContextAccesor;
    }
    public Guid? Id { get 
        {
            var value = _httpContextAccesor
                .HttpContext ?
                .User
                .FindFirst(JwtRegisteredClaimNames.Sub) ?
                .Value;

            return Guid.TryParse(value , out var userId) 
                ? userId
                : null ;
        } }

    public string? Email { get 
        {
            var email = _httpContextAccesor
                .HttpContext?
                .User
                .FindFirst(JwtRegisteredClaimNames.Email)?
                .Value;
            return email ?? null;
        } }

    public UserType? UserType { get 
        {
            var userType = _httpContextAccesor
                .HttpContext?
                .User
                .FindFirst(ClaimTypes.Role)?
                .Value;
            return Enum.TryParse<UserType>(userType, out var result)
           ? result
           : null;
        } }

    public bool IsAuthenticated => _httpContextAccesor
        .HttpContext?
        .User
        .Identity?
        .IsAuthenticated == true;

}
