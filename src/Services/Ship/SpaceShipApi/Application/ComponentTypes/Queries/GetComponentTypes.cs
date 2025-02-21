using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ComponentTypes.Queries;

public record GetComponentTypesQuery() : IRequest<List<ComponentTypeDto>>;

public class GetComponentTypes(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetComponentTypesQuery, List<ComponentTypeDto>>
{
    public async Task<List<ComponentTypeDto>> Handle(GetComponentTypesQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.ComponentTypes.ToListAsync(cancellationToken);
        return mapper.Map<List<ComponentTypeDto>>(entity);
    }
}