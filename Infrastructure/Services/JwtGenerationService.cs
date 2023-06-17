using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtGenerationService : IJwtGenerationService
{
    private readonly AuthenticationConfiguration _authenticationConfiguration;

    public JwtGenerationService(AuthenticationConfiguration authenticationConfiguration)
    {
        _authenticationConfiguration = authenticationConfiguration;
    }

    public string GenerateJwtToken(ClaimsIdentity subject)
    {
        JwtSecurityTokenHandler tokenHandler = new();

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = subject,
            Expires = DateTime.Now.AddMinutes(_authenticationConfiguration.LifeTime),
            SigningCredentials = new SigningCredentials(_authenticationConfiguration.SymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    //TODO Возможно стоит добавить роль для сотрудника
    public ClaimsIdentity AssembleClaimsIdentity(User user)
    {
        var subject = new ClaimsIdentity(new[]
        {
            new Claim(IdentityConfiguration.IdClaim, user.Id.ToString()),
            new Claim(ClaimsIdentity.DefaultIssuer, _authenticationConfiguration.Issuer),
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            // new Claim(ClaimsIdentity.DefaultRoleClaimType, employee.Role),
        });

        return subject;
    }
}