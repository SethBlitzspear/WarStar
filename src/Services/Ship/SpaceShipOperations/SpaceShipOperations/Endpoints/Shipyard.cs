using Api.Infrastructure;
using Application.Dtos;
using Application.Shipyard.Commands.AddComponent;
using Application.Shipyard.Commands.CreateSpaceship;
using Application.Shipyard.Queries;
using AutoMapper;
using MediatR;

namespace Api.Endpoints;

public sealed class Shipyard : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateSpaceShip, "Create")
            .MapGet(GetSpaceShips, "GetOwnerSpaceShips/{userId}")
            .MapGet(GetSpaceShipDetail, "GetSpaceShip/{spaceShipId}")
            .MapGet(GetComponentTypes, "GetComponentTypes")
            .MapGet(GetComponentType, "GetComponentType/{componentTypeId}")
            .MapPost(AddComponent, "AddComponent");


    }

    private static async Task<Guid> CreateSpaceShip(ISender sender, CreateSpaceShipDto spaceShip, IMapper mapper, CancellationToken cancellationToken)
    {
        return await sender.Send(mapper.Map<CreateSpaceShipCommand>(spaceShip), cancellationToken);
    }
    private static async Task<List<SpaceShipDto>> GetSpaceShips(ISender sender, Guid userId, IMapper mapper, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetUserSpaceShipsQuery() { UserId = userId}, cancellationToken);
    }
    private static async Task<SpaceShipDetailDto> GetSpaceShipDetail(ISender sender, Guid spaceShipId, IMapper mapper, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetSpaceShipDetailQuery() { SpaceShipId = spaceShipId }, cancellationToken);
    }
    private static async Task<List<ComponentTypeDto>> GetComponentTypes(ISender sender, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetComponentTypesQuery(), cancellationToken);
    }
    private static async Task<ComponentTypeDto> GetComponentType(ISender sender, Guid componentTypeId, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetComponentTypeQuery() { ComponentTypeId = componentTypeId }, cancellationToken);
    }
    private static async Task<Guid> AddComponent(ISender sender, ComponentDto addComponentDto, CancellationToken cancellationToken)
    {
        return await sender.Send(new AddComponentCommand() { ComponentDto = addComponentDto}, cancellationToken);
    }
}
