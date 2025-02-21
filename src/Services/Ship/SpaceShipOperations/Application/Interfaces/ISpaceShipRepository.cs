using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces;

public interface ISpaceShipRepository
{
    Task<Guid> CreateSpaceShip(SpaceShip spaceShip);
    public Task<SpaceShipDto?> GetSpaceShip(Guid spaceShipId);
    Task<List<SpaceShipDto>> GetSpaceShips();
    Task<Guid> CreateComponent(Component component);
    Task<List<ComponentDto>> GetComponents();
    public Task<List<ComponentTypeDto>> GetComponentTypes();
    public Task<List<ComponentDto>> GetSpaceShipComponents(Guid spaceShipId);
    public Task<ComponentDto> GetComponent(Guid componentId);
    public Task<ComponentDto> UpdateComponent(ComponentDto componentDto);
    public Task<ComponentTypeDto?> GetComponentType(string name);
    public Task<ComponentTypeDto?> GetComponentType(Guid componentTypeId);
}
