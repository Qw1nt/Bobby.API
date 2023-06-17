using Application.Common.Interfaces;
using Application.Identity.Commands.Authorized;
using Application.Identity.Commands.Register;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IHashSaltService _hashSaltService;
    private readonly JwtGenerationService _jwtGenerationService;
    private readonly IApplicationDataContext _dataContext;

    public AuthenticationService(IHashSaltService hashSaltService, JwtGenerationService jwtGenerationService,
        IApplicationDataContext dataContext)
    {
        _hashSaltService = hashSaltService;
        _dataContext = dataContext;
        _jwtGenerationService = jwtGenerationService;
    }

    public async Task<User> RegisterAsync(RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        User user = new()
        {
            Email = command.Email,
            PasswordHash = command.Password,
            Salt = _hashSaltService.Salt(),
        };

        user.PasswordHash = _hashSaltService.Hash(user.PasswordHash, user.Salt);

        var entry = await _dataContext.Users.AddAsync(user, cancellationToken);
        return entry.Entity;
    }

    //TODO Добавить валидацию
    public async Task<IdentityKeyValuePair> LoginAsync(AuthenticateCommand command,
        CancellationToken cancellationToken = default)
    {
        var employee = await _dataContext.Users
            .AsNoTracking()
            .FirstAsync(u => u.Email == command.Email, cancellationToken: cancellationToken);

        var claims = _jwtGenerationService.AssembleClaimsIdentity(employee);
        return new IdentityKeyValuePair {AccessToken = _jwtGenerationService.GenerateJwtToken(claims)};
    }
}