using System.Reflection;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDataContext : DbContext, IApplicationDataContext
{
    public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(options => options.MigrationsAssembly("API"));
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<GameUnit> GameUnits => Set<GameUnit>();

    public DbSet<UnityWorld> GameWorlds => Set<UnityWorld>();
    
    public DbSet<UnityGameObject> UnityGameObjects => Set<UnityGameObject>();
}