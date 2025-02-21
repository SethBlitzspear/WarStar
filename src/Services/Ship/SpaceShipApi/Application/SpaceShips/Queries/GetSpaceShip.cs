using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.SpaceShips.Queries;
public sealed record GetSpaceShipQuery(Guid Id) : IRequest<SpaceShipDto>;

public sealed class GetSpaceShip(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetSpaceShipQuery, SpaceShipDto>
{
    public async Task<SpaceShipDto> Handle(GetSpaceShipQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.SpaceShips.FindAsync(request.Id, cancellationToken);
        return mapper.Map<SpaceShipDto>(entity);
    }
}