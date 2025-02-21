using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces;
public interface IShipYardService
{
    public Task<Guid> CreateSpaceShip(CreateSpaceShipDto spaceShipDto);
    Task<List<SpaceShipDto>> GetUserSpaceShips(Guid userId);
    public Task<List<ComponentDto>> GetSpaceShipComponents(Guid spaceShipId);
    public Task<SpaceShipDto?> GetSpaceShip(Guid spaceShipId);
    public Task<List<ComponentType>> GetComponentTypes();
    public Task<ComponentType?> GetComponentType(Guid componentTypeId);
    public Task<ComponentType?> GetComponentType(string name);
    public Task<Guid> AddComponent(ComponentDto componentDto);
    public Task<ComponentDto> GetComponent(Guid componentId);
    public Task<ComponentDto> UpdateComponent(ComponentDto componentDto);
}