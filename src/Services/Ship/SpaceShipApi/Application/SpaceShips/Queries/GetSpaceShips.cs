using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.SpaceShips.Queries;
public sealed record GetSpaceShipsQuery() : IRequest<List<SpaceShipDto>>;

public sealed class GetSpaceShips(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetSpaceShipsQuery, List<SpaceShipDto>>
{
    public async Task<List<SpaceShipDto>> Handle(GetSpaceShipsQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.SpaceShips.ToListAsync(cancellationToken);
        return mapper.Map<List<SpaceShipDto>>(entity);
    }
}