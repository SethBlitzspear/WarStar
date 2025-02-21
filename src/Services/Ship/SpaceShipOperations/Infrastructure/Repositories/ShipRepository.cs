using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System.Net.Http.Json;

namespace Infrastructure.Repositories;

public class ShipRepository(HttpClient httpClient, IMapper mapper) : IShipRepository
{
    public async Task<Guid> CreateSpaceShip(SpaceShip spaceShip)
    {
        var response = await httpClient.PostAsJsonAsync("api/spaceship", spaceShip);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return Guid.Parse(responseContent.Trim('"'));
    }

    public async Task<List<SpaceShip>> GetSpaceShips()
    {
        var response = await httpClient.GetFromJsonAsync<List<SpaceShipDto>>("api/spaceship/");
        return mapper.Map<List<SpaceShip>>(response) ?? [];
    }
    public async Task<SpaceShip?> GetSpaceShip(Guid spaceShipId)
    {
        var response = await httpClient.GetFromJsonAsync<SpaceShipDto>($"api/spaceship/{spaceShipId}");
        return mapper.Map<SpaceShip?>(response);
    }

    public async Task<Guid> CreateComponent(Component component)
    {
        var response = await httpClient.PostAsJsonAsync("api/component", component);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    public async Task<Component> GetComponent(Guid componentId)
    {
        var response = await httpClient.GetFromJsonAsync<Component>($"api/component/{componentId}");
        return mapper.Map<Component>(response) ?? new Component();
    }
    public async Task<Component> UpdateComponent(Component component)
    {
        var response = await httpClient.PutAsJsonAsync($"api/component/{component.Id}", component);

        return await response.Content.ReadFromJsonAsync<Component>() ?? new Component();
    }

    public async Task<List<ComponentType>> GetComponentTypes()
    {
        var response = await httpClient.GetFromJsonAsync<List<ComponentTypeDto>>("api/componenttype");
        return mapper.Map<List<ComponentType>>(response) ?? [];
    }
    public async Task<List<Component>> GetComponents()
    {
        var response = await httpClient.GetFromJsonAsync<List<ComponentDto>>("api/component");
        return mapper.Map<List<Component>>(response) ?? [];
    }

    public async Task<List<Component>> GetSpaceShipComponents(Guid spaceShipId)
    {
        var response = await httpClient.GetFromJsonAsync<List<ComponentDto>>($"api/component/SpaceShip/{spaceShipId}");
        return mapper.Map<List<Component>>(response) ?? [];
    }

    public async Task<ComponentType?> GetComponentType(string name)
    {
        var componentTypes = await GetComponentTypes();

        return componentTypes?.Find(ct => ct.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<ComponentType?> GetComponentType(Guid componentTypeId)
    {
        var componentTypes = await GetComponentTypes();

        return componentTypes?.Find(ct => ct.Id.Equals(componentTypeId));
    }
}