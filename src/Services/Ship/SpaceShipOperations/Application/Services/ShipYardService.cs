using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;
public class ShipYardService(IShipRepository spaceShipRepository, IMapper mapper) : IShipYardService
{
    public async Task<Guid> CreateSpaceShip(CreateSpaceShipDto spaceShipDto)
    {
       var spaceShip = mapper.Map<SpaceShip>(spaceShipDto);
        spaceShip.Components = [];

        spaceShip.Id = await spaceShipRepository.CreateSpaceShip(spaceShip);
        var keelComponentType = await GetComponentType("Keel");

        await spaceShipRepository.CreateComponent(new Component { 
            SpaceShipId = spaceShip.Id,
            ComponentTypeId = keelComponentType?.Id ?? Guid.Empty,
            UpConnection = true,
            DownConnection = true,
            LeftConnection = true,
            RightConnection = true,
            UpPowerCoupling = true,
            DownPowerCoupling = true,
            LeftPowerCoupling = true,
            RightPowerCoupling = true
        });

        return spaceShip.Id;
    }

    public async Task<List<SpaceShip>> GetUserSpaceShips(Guid userId)
    {
        var ships = await spaceShipRepository.GetSpaceShips();
        return ships.FindAll(s => s.OwnerId == userId);
    }

    public async Task<SpaceShip?> GetSpaceShip(Guid spaceShipId)
    {
        return await spaceShipRepository.GetSpaceShip(spaceShipId);
    }

    public async Task<List<Component>> GetSpaceShipComponents(Guid spaceShipId)
    {
        var components = await spaceShipRepository.GetSpaceShipComponents(spaceShipId);

        var componentLookup = components.ToDictionary(c => c.Id);

        foreach (var component in components)
        {
            component.TopComponent = component.TopComponentId.HasValue
                ? componentLookup.GetValueOrDefault(component.TopComponentId.Value)
                : null;
            component.BottomComponent = component.BottomComponentId.HasValue
                ? componentLookup.GetValueOrDefault(component.BottomComponentId.Value)
                : null;
            component.LeftComponent = component.LeftComponentId.HasValue
                ? componentLookup.GetValueOrDefault(component.LeftComponentId.Value)
                : null;
            component.RightComponent = component.RightComponentId.HasValue
                ? componentLookup.GetValueOrDefault(component.RightComponentId.Value)
                : null;
        }
        return components;
    }

    public async Task<List<ComponentType>> GetComponentTypes()
    {
        return await spaceShipRepository.GetComponentTypes();
    }

    public async Task<ComponentType?> GetComponentType(Guid ComponentTypeId)
    {
        return await spaceShipRepository.GetComponentType(ComponentTypeId);
    }

    public async Task<ComponentType?> GetComponentType(string name)
    {
        return mapper.Map<ComponentType>(await spaceShipRepository.GetComponentType(name));
    }

    public async Task<Guid> AddComponent(Component component)
    {
        var componentType = await spaceShipRepository.GetComponentType(component.ComponentTypeId);
        if (componentType == null)
        {
            throw new Exception("Invalid Component Id " + component.ComponentTypeId);
        }

        component.Armour = componentType.Armour;
        component.StructuralIntegrity = componentType.StructuralIntegrity;
        component.LifeSupport = componentType.LifeSupport;
        component.MinPowerDraw = componentType.MinPowerDraw;
        component.MaxPowerDraw = componentType.MaxPowerDraw;
        component.Mass = componentType.Mass;
        component.Price = componentType.Price;
        component.Properties = componentType.Properties;


        component.Id = await spaceShipRepository.CreateComponent(component);

        return component.Id;
    }

    public async Task<Component> GetComponent(Guid componentId)
    {
        return await spaceShipRepository.GetComponent(componentId);
    }

    public async Task<Component> UpdateComponent(Component component)
    {
        return await spaceShipRepository.UpdateComponent(component);
    }
}