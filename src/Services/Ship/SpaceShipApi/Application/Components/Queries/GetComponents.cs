using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Components.Queries;
public record GetComponentsQuery() : IRequest<List<ComponentDto>>;

public class GetComponents(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetComponentsQuery, List<ComponentDto>>
{
    public async Task<List<ComponentDto>> Handle(GetComponentsQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.SpaceShips.ToListAsync(cancellationToken);
        return mapper.Map<List<ComponentDto>>(entity);
    }
}