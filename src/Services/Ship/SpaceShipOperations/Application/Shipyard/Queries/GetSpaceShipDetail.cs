using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Shipyard.Queries;

public class GetSpaceShipDetailQuery : IRequest<SpaceShipDetailDto>
{
    public Guid SpaceShipId { get; set; }
}

public class GetSpaceShipDetail(IShipYardService shipYardService, IShipLayoutService layoutService, IMapper mapper) : IRequestHandler<GetSpaceShipDetailQuery, SpaceShipDetailDto>
{
    public async Task<SpaceShipDetailDto> Handle(GetSpaceShipDetailQuery request, CancellationToken cancellationToken)
    {
        var components = ReconcileComponentReferences(mapper.Map<List<Component>>(await shipYardService.GetSpaceShipComponents(request.SpaceShipId)));
        var layoutComponents = layoutService.BuildShipLayout(mapper.Map<List<Component>>(components));

        var spaceShip = await shipYardService.GetSpaceShip(request.SpaceShipId);
        var spaceShipDto = mapper.Map<SpaceShipDetailDto>(spaceShip);
        spaceShipDto.ShipLayout = mapper.Map<ComponentDto[][]>(layoutComponents);

        return spaceShipDto;
    }

    private static List<Component> ReconcileComponentReferences(List<Component> components)
    {
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
}
