using Api.Infrastructure;
using Application.Common.Dtos;
using Application.Components.Commands;
using Application.Components.Queries;
using Azure.Core;
using MediatR;

namespace Api.Endpoints;

public sealed class Component : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateComponent)
            .MapGet(GetComponent, "{id}")
            .MapGet(GetComponents)
            .MapGet(GetSpaceShipComponents, "/SpaceShip/{id}")
            .MapPut(UpdateComponent, "{id}")
        .MapDelete(DeleteAccount, "{id}");
    }

    private static async Task<Guid> CreateComponent(ISender sender, ComponentDto componentDto, CancellationToken cancellationToken)
    {
        return await sender.Send(new CreateComponentCommand { ComponentData = componentDto }, cancellationToken);
    }

    private static async Task<ComponentDto> GetComponent(ISender sender, Guid id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetComponentQuery(id), cancellationToken);
    }

    private static async Task<List<ComponentDto>> GetComponents(ISender sender, CancellationToken cancellationToken)
    {
      return await sender.Send(new GetComponentsQuery(), cancellationToken);
    }

    private static async Task<List<ComponentDto>> GetSpaceShipComponents(ISender sender, Guid id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetSpaceShipComponentsQuery(id), cancellationToken);
    }


    private static async Task<ComponentDto> UpdateComponent(ISender sender, Guid id, ComponentDto componentDto, CancellationToken cancellationToken)
    {
      return await sender.Send(new UpdateComponentCommand { ComponentDto = componentDto, Id = id }, cancellationToken);
    }

    private static async Task<bool> DeleteAccount(ISender sender, Guid id)
    {
       return await sender.Send(new DeleteComponentCommand(id));
    }
}
