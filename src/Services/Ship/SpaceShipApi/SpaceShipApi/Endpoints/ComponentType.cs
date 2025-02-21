using Api.Infrastructure;
using Application.Common.Dtos;
using Application.ComponentTypes.Queries;
using MediatR;

namespace Api.Endpoints;

public sealed class ComponentType : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetComponentTypes);
    }

    private static async Task<List<ComponentTypeDto>> GetComponentTypes(ISender sender, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetComponentTypesQuery(), cancellationToken);
    }
}
