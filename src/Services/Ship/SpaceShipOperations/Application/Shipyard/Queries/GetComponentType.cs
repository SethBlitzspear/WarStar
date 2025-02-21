using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Shipyard.Queries;

public record GetComponentTypeQuery : IRequest<ComponentTypeDto>
{
    public Guid ComponentTypeId { get; set; }
}

public class GetComponentType(IShipYardService shipYardService, IMapper mapper) : IRequestHandler<GetComponentTypeQuery, ComponentTypeDto>
{
    public async Task<ComponentTypeDto> Handle(GetComponentTypeQuery request, CancellationToken cancellationToken)
    {
        var componentType = await shipYardService.GetComponentType(request.ComponentTypeId);
        return mapper.Map<ComponentTypeDto>(componentType);
    }
}