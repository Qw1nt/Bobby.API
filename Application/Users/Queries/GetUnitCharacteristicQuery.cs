using Application.Common.Interfaces;
using Domain.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries;

public record GetUnitCharacteristicQuery(int UserId) : IRequest<GameUnit?>;

public class GetUnitCharacteristicQueryHandler : IRequestHandler<GetUnitCharacteristicQuery, GameUnit?>
{
    private readonly IApplicationDataContext _applicationDataContext;

    public GetUnitCharacteristicQueryHandler(IApplicationDataContext applicationDataContext)
    {
        _applicationDataContext = applicationDataContext;
    }

    public async ValueTask<GameUnit?> Handle(GetUnitCharacteristicQuery request, CancellationToken cancellationToken)
    {
        var user = await _applicationDataContext.Users
            .AsNoTracking()
            .Include(x => x.Units)
            .FirstOrDefaultAsync(x => x.Id == request.UserId,
            cancellationToken: cancellationToken);

        return user?.Units[0];
    }
}