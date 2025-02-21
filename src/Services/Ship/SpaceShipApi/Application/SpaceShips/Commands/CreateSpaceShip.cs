using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.SpaceShips.Commands;

public record CreateSpaceShipCommand() : IRequest<Guid>
{
    public required string Name { get; init; }
    public Guid OwnerId { get; init; }
}

public class CreateSpaceShip(IApplicationDbContext context) : IRequestHandler<CreateSpaceShipCommand, Guid>
{

    public async Task<Guid> Handle(CreateSpaceShipCommand request, CancellationToken cancellationToken)
    {
        var entity = new SpaceShip
        {
            Name = request.Name,
            OwnerId = request.OwnerId
        };

        await context.SpaceShips.AddAsync(entity, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}