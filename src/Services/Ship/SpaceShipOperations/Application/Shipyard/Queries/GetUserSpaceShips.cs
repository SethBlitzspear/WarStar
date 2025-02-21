using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Shipyard.Queries;
public class GetUserSpaceShipsQuery : IRequest<List<SpaceShipDto>>
{
    public Guid UserId { get; set; }
}

public class GetUserSpaceShips(IShipYardService shipYardService) : IRequestHandler<GetUserSpaceShipsQuery, List<SpaceShipDto>>
{
    public async Task<List<SpaceShipDto>> Handle(GetUserSpaceShipsQuery request, CancellationToken cancellationToken)
    {
        return await shipYardService.GetUserSpaceShips(request.UserId);
    }
}