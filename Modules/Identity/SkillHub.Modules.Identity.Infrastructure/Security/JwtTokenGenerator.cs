using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SkillHub.Modules.Identity.Application.Abstractions.Security;
using SkillHub.Modules.Identity.Application.Commands.LoginUser;
using SkillHub.Modules.Identity.Domain.Enums;
using SkillHub.Modules.Identity.Infrastructure.Configuration;

namespace SkillHub.Modules.Identity.Infrastructure.Security;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions _opt;
    public JwtTokenGenerator(IOptions<JwtOptions> opt)
    {
        _opt = opt.Value;
    }
    public JwtTokenResult GenerateToken(Guid userId, string email, UserType type)
    {

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, userId.ToString()),
            new (JwtRegisteredClaimNames.Email, email),
            new (ClaimTypes.Role, type.ToString())
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiresAt = DateTime.UtcNow.AddMinutes(_opt.ExpirationMinutes);

        var token = new JwtSecurityToken(
            issuer: _opt.Issuer,
            audience: _opt.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials : credentials
            );

        var accessToken = new JwtSecurityTokenHandler()
        .WriteToken(token);

        return new JwtTokenResult(
        accessToken,
        expiresAt);
    }
}
