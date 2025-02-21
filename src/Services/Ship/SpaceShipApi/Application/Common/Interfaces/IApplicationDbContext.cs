using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;
public interface IApplicationDbContext : IDisposable
{
    DbSet<SpaceShip> SpaceShips { get; }
    DbSet<Component> Components { get; }
    DbSet<ComponentType> ComponentTypes { get; }
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
