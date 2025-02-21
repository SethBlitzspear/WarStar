using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using System.Data.Common;

namespace Application.Services;
public class ShipYardService(ISpaceShipRepository spaceShipRepository, IMapper mapper, IMemoryCache Cache) : IShipYardService
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

    public async Task<List<SpaceShipDto>> GetUserSpaceShips(Guid userId)
    {
        var ships = await spaceShipRepository.GetSpaceShips();
        return ships.FindAll(s => s.OwnerId == userId);
    }

    public async Task<SpaceShipDto?> GetSpaceShip(Guid spaceShipId)
    {
        return await spaceShipRepository.GetSpaceShip(spaceShipId);
    }

    public async Task<List<ComponentDto>> GetSpaceShipComponents(Guid spaceShipId)
    {
        var components = await spaceShipRepository.GetSpaceShipComponents(spaceShipId);
        var componentTypes = await GetComponentTypes();

        foreach (var component in components)
        {
            component.ComponentType = mapper.Map<ComponentTypeDto>(componentTypes.FirstOrDefault(ct => ct.Id == component.ComponentTypeId))
                ?? throw new InvalidOperationException($"ComponentType not found for ID {component.ComponentTypeId}");
        }

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
        const string cacheKey = "ComponentTypes";

        if (!Cache.TryGetValue(cacheKey, out List<ComponentType>? componentTypes))
        {
            componentTypes = mapper.Map<List<ComponentType>>(await spaceShipRepository.GetComponentTypes());
            Cache.Set(cacheKey, componentTypes);
        }

        return componentTypes ?? [];
    }

    public async Task<ComponentType?> GetComponentType(Guid ComponentTypeId)
    {
        return mapper.Map<ComponentType>(await spaceShipRepository.GetComponentType(ComponentTypeId));
    }

    public async Task<ComponentType?> GetComponentType(string name)
    {
        return mapper.Map<ComponentType>(await spaceShipRepository.GetComponentType(name));
    }

    public async Task<Guid> AddComponent(ComponentDto componentDto)
    {
        var component = mapper.Map<Component>(componentDto);
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

    public async Task<ComponentDto> GetComponent(Guid componentId)
    {
        return await spaceShipRepository.GetComponent(componentId);
    }

    public async Task<ComponentDto> UpdateComponent(ComponentDto componentDto)
    {
        return await spaceShipRepository.UpdateComponent(componentDto);
    }
}