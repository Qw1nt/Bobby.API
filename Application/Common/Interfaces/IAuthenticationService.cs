using Application.Identity.Commands.Authorized;
using Application.Identity.Commands.Register;
using Domain.Common;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IAuthenticationService
{
    Task<User> RegisterAsync(RegisterUserCommand command, CancellationToken cancellationToken = default);

    Task<IdentityKeyValuePair> LoginAsync(AuthenticateCommand command, CancellationToken cancellationToken = default);
}