using Application.Common.Interfaces;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Application.UnityWorlds.Commands.AddExtractedResourcesCost;

public record AddExtractedResourcesCostCommand(int UnityWorldId, float Amount) : IRequest<bool>;

public class AddExtractedResourcesCostCommandHandler : IRequestHandler<AddExtractedResourcesCostCommand, bool>
{
    private readonly IApplicationDataContext _applicationDataContext;

    public AddExtractedResourcesCostCommandHandler(IApplicationDataContext applicationDataContext)
    {
        _applicationDataContext = applicationDataContext;
    }

    public async ValueTask<bool> Handle(AddExtractedResourcesCostCommand request, CancellationToken cancellationToken)
    {
        var world = await _applicationDataContext.UnityWorlds
            .FirstOrDefaultAsync(x => x.Id == request.UnityWorldId, cancellationToken: cancellationToken);

        if (world == null)
            return false;
        
        world.ExtractedResourcesCost += request.Amount;

        _applicationDataContext.UnityWorlds.Update(world);
        await _applicationDataContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}