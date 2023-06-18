using Application.Common.Interfaces;
using Domain.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Application.UnityWorlds.Commands.MockSave;

public record UnityWorldSaveCommand(string Name, int SceneIndex, List<RequestUnityWorldGameObject> Objects) : IRequest<UnityWorld>;

public class UnityWorldSaveCommandHandler : IRequestHandler<UnityWorldSaveCommand, UnityWorld>
{
    private readonly IApplicationDataContext _applicationDataContext;

    public UnityWorldSaveCommandHandler(IApplicationDataContext applicationDataContext)
    {
        _applicationDataContext = applicationDataContext;
    }

    public async ValueTask<UnityWorld> Handle(UnityWorldSaveCommand request, CancellationToken cancellationToken)
    {
        var objects = await _applicationDataContext.UnityGameObjects
            .ToListAsync(cancellationToken: cancellationToken);

        var gameObjectsDictionary = objects.ToDictionary(gameObject => gameObject.IdInUnity);

        List<UnityWorldGameObject> worldGameObjects = new(request.Objects.Count);
        foreach (var worldGameObject in request.Objects)
        {
            worldGameObjects.Add(new UnityWorldGameObject
            {
                PositionX = worldGameObject.PositionX,
                PositionY = worldGameObject.PositionY,
                PositionZ = worldGameObject.PositionZ,          
                RotationX = worldGameObject.RotationX,
                RotationY = worldGameObject.RotationY,
                RotationZ = worldGameObject.RotationZ,
                Scale = worldGameObject.Scale,
                UnityReference = gameObjectsDictionary[worldGameObject.UnityGameObjectId],
            });
        }

        await _applicationDataContext.UnityWorldGameObjects.AddRangeAsync(worldGameObjects, cancellationToken);

        var world = new UnityWorld
        {
            Name = request.Name,
            SceneIndex = request.SceneIndex,
            DateOfCreation = DateTime.UtcNow,
            Objects = worldGameObjects
        };
        
        var worldEntry = await _applicationDataContext.UnityWorlds.AddAsync(world, cancellationToken);
        await _applicationDataContext.SaveChangesAsync(cancellationToken);

        return worldEntry.Entity;
    }
}