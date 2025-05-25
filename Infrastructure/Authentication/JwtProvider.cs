
using Application.Common.Abstractions;
using Domain.Members.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    public JwtProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }
    public string Generate(Member member)
    {
        var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, member.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, member.Email),
                    new Claim("Role", member.AccountType.ToString())
                },
                    expires: DateTime.UtcNow.AddDays(_jwtOptions.ExpireInDays),
                    signingCredentials: new SigningCredentials(
                        _jwtOptions.GetSymmetricSecurityKey(),
                        SecurityAlgorithms.HmacSha256
                        )
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
