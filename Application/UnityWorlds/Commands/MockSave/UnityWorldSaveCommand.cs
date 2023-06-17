using Application.Common.Interfaces;
using Domain.Entities;
using Mediator;

namespace Application.UnityWorlds.Commands.MockSave;

public record UnityWorldSaveCommand(string Name, int SceneIndex, List<UnityWorldGameObject> Objects) : IRequest<UnityWorld>;

public class UnityWorldSaveCommandHandler : IRequestHandler<UnityWorldSaveCommand, UnityWorld>
{
    private readonly IApplicationDataContext _applicationDataContext;

    public UnityWorldSaveCommandHandler(IApplicationDataContext applicationDataContext)
    {
        _applicationDataContext = applicationDataContext;
    }

    public async ValueTask<UnityWorld> Handle(UnityWorldSaveCommand request, CancellationToken cancellationToken)
    {
        var world = new UnityWorld
        {
            Name = request.Name,
            SceneIndex = request.SceneIndex,
            DateOfCreation = DateTime.UtcNow,
            Objects = request.Objects
        };

        var entry = await _applicationDataContext.GameWorlds.AddAsync(world, cancellationToken);
        await _applicationDataContext.SaveChangesAsync(cancellationToken);

        return entry.Entity;
    }
}