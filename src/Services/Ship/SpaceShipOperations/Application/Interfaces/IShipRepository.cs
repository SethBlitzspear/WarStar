using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces;

public interface IShipRepository
{
    Task<Guid> CreateSpaceShip(SpaceShip spaceShip);
    public Task<SpaceShip?> GetSpaceShip(Guid spaceShipId);
    Task<List<SpaceShip>> GetSpaceShips();
    Task<Guid> CreateComponent(Component component);
    Task<List<Component>> GetComponents();
    public Task<List<ComponentType>> GetComponentTypes();
    public Task<List<Component>> GetSpaceShipComponents(Guid spaceShipId);
    public Task<Component> GetComponent(Guid componentId);
    public Task<Component> UpdateComponent(Component component);
    public Task<ComponentType?> GetComponentType(string name);
    public Task<ComponentType?> GetComponentType(Guid componentTypeId);
}
