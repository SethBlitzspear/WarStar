using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.SpaceShips.Commands;
public record DeleteSpaceShipCommand(Guid Id) : IRequest<bool>;

public class DeleteSpaceShip(IApplicationDbContext context) : IRequestHandler<DeleteSpaceShipCommand, bool>
{
    public async Task<bool> Handle(DeleteSpaceShipCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await context.SpaceShips.FindAsync(request.Id, cancellationToken) ??
                         throw new EntityNotFoundException($"Unable to find Space Ship with ID of {request.Id}");
            context.SpaceShips.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}
