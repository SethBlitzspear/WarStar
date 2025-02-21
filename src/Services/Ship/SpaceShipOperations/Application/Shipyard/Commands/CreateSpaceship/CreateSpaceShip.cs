using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Shipyard.Commands.CreateSpaceship;
public record CreateSpaceShipCommand() : IRequest<Guid>
{
    public required string Name { get; set; }
    public Guid OwnerId { get; set; }
}
public class CreateSpaceShip(IShipYardService shipYardService, IMapper mapper) : IRequestHandler<CreateSpaceShipCommand, Guid>
{
    public async Task<Guid> Handle(CreateSpaceShipCommand request, CancellationToken cancellationToken)
    {
        return await shipYardService.CreateSpaceShip(mapper.Map<CreateSpaceShipDto>(request));
    }
}
