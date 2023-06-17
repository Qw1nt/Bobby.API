using Application.Shared;
using FluentValidation;

namespace Application.Identity.Commands.Authorized;

public class AuthenticateEmployeeCommandValidator : AbstractValidator<IdentityCommand>
{
    public AuthenticateEmployeeCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty();
    }
}