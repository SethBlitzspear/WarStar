using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Shipyard.Queries;
public record GetComponentTypesQuery : IRequest<List<ComponentTypeDto>>
{
}

public class GetComponentTypes(IShipYardService shipYardService, IMapper mapper) : IRequestHandler<GetComponentTypesQuery, List<ComponentTypeDto>>
{
    public async Task<List<ComponentTypeDto>> Handle(GetComponentTypesQuery request, CancellationToken cancellationToken)
    {
        var components =  await shipYardService.GetComponentTypes();
        return mapper.Map<List<ComponentTypeDto>>(components);
    }
}