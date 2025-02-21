using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;

namespace Application.Shipyard.Queries;
public class GetUserSpaceShipsQuery : IRequest<List<SpaceShipDto>>
{
    public Guid UserId { get; set; }
}

public class GetUserSpaceShips(IShipYardService shipYardService, IMapper mapper) : IRequestHandler<GetUserSpaceShipsQuery, List<SpaceShipDto>>
{
    public async Task<List<SpaceShipDto>> Handle(GetUserSpaceShipsQuery request, CancellationToken cancellationToken)
    {
        return mapper.Map<List<SpaceShipDto>>(await shipYardService.GetUserSpaceShips(request.UserId));
    }
}