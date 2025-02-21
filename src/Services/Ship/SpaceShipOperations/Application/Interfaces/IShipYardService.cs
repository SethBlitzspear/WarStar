using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces;
public interface IShipYardService
{
    public Task<Guid> CreateSpaceShip(CreateSpaceShipDto spaceShipDto);
    Task<List<SpaceShip>> GetUserSpaceShips(Guid userId);
    public Task<List<Component>> GetSpaceShipComponents(Guid spaceShipId);
    public Task<SpaceShip?> GetSpaceShip(Guid spaceShipId);
    public Task<List<ComponentType>> GetComponentTypes();
    public Task<ComponentType?> GetComponentType(Guid componentTypeId);
    public Task<ComponentType?> GetComponentType(string name);
    public Task<Guid> AddComponent(Component component);
    public Task<Component> GetComponent(Guid componentId);
    public Task<Component> UpdateComponent(Component component);
}