using Api.Infrastructure;
using Application.Common.Dtos;
using Application.SpaceShips.Commands;
using Application.SpaceShips.Queries;
using MediatR;

namespace Api.Endpoints;

public sealed class SpaceShip : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateSpaceShip)
            .MapGet(GetSpaceShip, "{id}")
            .MapGet(GetSpaceShips)
            .MapPut(UpdateSpaceShip, "{id}")
            .MapDelete(DeleteSpaceShip, "{id}");
    }

    private static async Task<Guid> CreateSpaceShip(ISender sender, SpaceShipDto command, CancellationToken cancellationToken)
    {
        return await sender.Send(new CreateSpaceShipCommand
        {
            Name = command.Name,
            OwnerId = command.OwnerId
        }, cancellationToken);
    }

    private static async Task<SpaceShipDto> GetSpaceShip(ISender sender, Guid id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetSpaceShipQuery(id), cancellationToken);
    }

    private static async Task<List<SpaceShipDto>> GetSpaceShips(ISender sender, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetSpaceShipsQuery(), cancellationToken);
    }

    private static async Task<SpaceShipDto> UpdateSpaceShip(ISender sender, SpaceShipDto spaceShipDto, CancellationToken cancellationToken)
    {
        return await sender.Send(new UpdateSpaceShipCommand
        {
            Id = spaceShipDto.Id,
            Name = spaceShipDto.Name,
            OwnerId = spaceShipDto.OwnerId
        }, cancellationToken);
    }

    private static async Task<bool> DeleteSpaceShip(ISender sender, Guid id)
    {
        return await sender.Send(new DeleteSpaceShipCommand(id));
    }
}
