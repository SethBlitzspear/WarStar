using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace Infrastructure.Repositories;
public class SpaceShipRepository(HttpClient httpClient) : ISpaceShipRepository
{
    public async Task<Guid> CreateSpaceShip(SpaceShip spaceShip)
    {
        var response = await httpClient.PostAsJsonAsync("api/spaceship", spaceShip);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return Guid.Parse(responseContent.Trim('"'));
    }

    public async Task<List<SpaceShipDto>> GetSpaceShips()
    {
        var response = await httpClient.GetFromJsonAsync<List<SpaceShipDto>>("api/spaceship/");
        return response ?? [];
    }
    public async Task<SpaceShipDto?> GetSpaceShip(Guid spaceShipId)
    {
        var response = await httpClient.GetFromJsonAsync<SpaceShipDto>($"api/spaceship/{spaceShipId}");
        return response;
    }

    public async Task<Guid> CreateComponent(Component component)
    {
        var response = await httpClient.PostAsJsonAsync("api/component", component);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    public async Task<ComponentDto> GetComponent(Guid componentId)
    {
        var response = await httpClient.GetFromJsonAsync<ComponentDto>($"api/component/{componentId}");
        return response ?? new ComponentDto();
    }
    public async Task<ComponentDto> UpdateComponent(ComponentDto componentDto)
    {
        var response = await httpClient.PutAsJsonAsync($"api/component/{componentDto.Id}", componentDto);

        return await response.Content.ReadFromJsonAsync<ComponentDto>() ?? new ComponentDto();
    }

    public async Task<List<ComponentTypeDto>> GetComponentTypes()
    {
        var response = await httpClient.GetFromJsonAsync<List<ComponentTypeDto>>("api/componenttype");
        return response ?? [];
    }
    public async Task<List<ComponentDto>> GetComponents()
    {
        var response = await httpClient.GetFromJsonAsync<List<ComponentDto>>("api/component");
        return response ?? [];
    }

    public async Task<List<ComponentDto>> GetSpaceShipComponents(Guid spaceShipId)
    {
        var response = await httpClient.GetFromJsonAsync<List<ComponentDto>>($"api/component/SpaceShip/{spaceShipId}");
        return response ?? [];
    }

    public async Task<ComponentTypeDto?> GetComponentType(string name)
    {
        var componentTypes = await GetComponentTypes();

        return componentTypes?.Find(ct => ct.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<ComponentTypeDto?> GetComponentType(Guid componentTypeId)
    {
        var componentTypes = await GetComponentTypes();

        return componentTypes?.Find(ct => ct.Id.Equals(componentTypeId));
    }
}