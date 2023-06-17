using Application.Common.Interfaces;
using Application.Shared;
using Domain.Common;
using Domain.Entities;
using Mediator;

namespace Application.Identity.Commands.Authorized;

public class AuthenticateCommand : IdentityCommand, IRequest<IdentityKeyValuePair>
{
    
}

public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, IdentityKeyValuePair>
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticateCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async ValueTask<IdentityKeyValuePair> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.LoginAsync(request, cancellationToken);
        return result;
    }
}