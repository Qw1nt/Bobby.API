using Application.Common.Interfaces;
using Domain.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Application.UnityGameObjects.Commands;

public record AddUnityGameObjectCommand(int IdInUnity, string Name) : IRequest<bool>;

public class AddUnityGameObjectCommandHandler : IRequestHandler<AddUnityGameObjectCommand, bool>
{
    private readonly IApplicationDataContext _applicationDataContext;

    public AddUnityGameObjectCommandHandler(IApplicationDataContext applicationDataContext)
    {
        _applicationDataContext = applicationDataContext;
    }

    public async ValueTask<bool> Handle(AddUnityGameObjectCommand request, CancellationToken cancellationToken)
    {
        var alreadyExist = await _applicationDataContext.UnityGameObjects
            .AsNoTracking()
            .AnyAsync(x => x.IdInUnity == request.IdInUnity, cancellationToken: cancellationToken);

        if (alreadyExist == true)
            return false;

        await _applicationDataContext.UnityGameObjects
            .AddAsync(new UnityGameObject {IdInUnity = request.IdInUnity, Name = request.Name}, cancellationToken);
        await _applicationDataContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}