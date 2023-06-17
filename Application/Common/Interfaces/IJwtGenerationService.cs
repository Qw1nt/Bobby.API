using System.Security.Claims;

namespace Application.Common.Interfaces;

public interface IJwtGenerationService
{
    string GenerateJwtToken(ClaimsIdentity subject);
}