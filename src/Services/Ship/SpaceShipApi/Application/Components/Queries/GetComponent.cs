using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Components.Queries;
public sealed record GetComponentQuery(Guid Id) : IRequest<ComponentDto>;

public sealed class GetComponent(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetComponentQuery, ComponentDto>
{
    public async Task<ComponentDto> Handle(GetComponentQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Components.FindAsync(request.Id, cancellationToken);
        return mapper.Map<ComponentDto>(entity);
    }
}