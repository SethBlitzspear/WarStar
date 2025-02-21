using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Components.Queries;
public record GetSpaceShipComponentsQuery(Guid Id) : IRequest<List<ComponentDto>>;

public class GetSpaceShipComponents(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetSpaceShipComponentsQuery, List<ComponentDto>>
{
    public async Task<List<ComponentDto>> Handle(GetSpaceShipComponentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await context.Components
            .Where(c => c.SpaceShipId == request.Id)
            .ToListAsync(cancellationToken);

        return mapper.Map<List<ComponentDto>>(entities);
    }
}
