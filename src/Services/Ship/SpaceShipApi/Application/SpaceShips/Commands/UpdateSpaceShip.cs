using Application.Common.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.SpaceShips.Commands;

public record UpdateSpaceShipCommand() : IRequest<SpaceShipDto>
{
    public Guid? Id { get; init; }
    public required string Name { get; init; }
    public Guid OwnerId { get; init; }
}

public class UpdateSpaceShip(IApplicationDbContext context, IMapper mapper) : IRequestHandler<UpdateSpaceShipCommand, SpaceShipDto>
{
    public async Task<SpaceShipDto> Handle(UpdateSpaceShipCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.SpaceShips.FindAsync(request.Id, cancellationToken) ?? throw new EntityNotFoundException($"Unable to find Space Ship with ID of {request.Id}");

        entity.Name = request.Name;
        entity.OwnerId = request.OwnerId;

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<SpaceShipDto>(entity);
    }
}
