using Application.Common.Interfaces;
using Application.Shared;
using Domain.Entities;
using Mediator;

namespace Application.Identity.Commands.Register;

public class RegisterUserCommand : IdentityCommand, IRequest<User>
{
    
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IApplicationDataContext _applicationDataContext;

    public RegisterUserCommandHandler(IAuthenticationService authenticationService, IApplicationDataContext applicationDataContext)
    {
        _authenticationService = authenticationService;
        _applicationDataContext = applicationDataContext;
    }

    public async ValueTask<User> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var response = await _authenticationService.RegisterAsync(command, cancellationToken);
        
        await _applicationDataContext.GameUnits.AddAsync(GameUnit.Create(), cancellationToken);
        await _applicationDataContext.SaveChangesAsync(cancellationToken);
        
        return response;
    }
}