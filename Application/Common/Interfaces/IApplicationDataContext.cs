using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common.Interfaces;

public interface IApplicationDataContext
{
    public DatabaseFacade Database { get; }
    
    public DbSet<User> Users { get; }
    
    public DbSet<GameUnit> GameUnits { get; }
    
    public DbSet<UnityWorld> UnityWorlds { get; }
    
    public DbSet<UnityGameObject> UnityGameObjects { get; }
    
    public DbSet<UnityWorldGameObject> UnityWorldGameObjects { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}