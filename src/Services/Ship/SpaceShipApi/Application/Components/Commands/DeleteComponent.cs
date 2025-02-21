using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Components.Commands;

public sealed record DeleteComponentCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteComponent(IApplicationDbContext context) : IRequestHandler<DeleteComponentCommand, bool>
{
    public async Task<bool> Handle(DeleteComponentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await context.Components.FindAsync(request.Id, cancellationToken) ??
                         throw new EntityNotFoundException($"Unable to find Component with ID of {request.Id}");
            context.Components.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}  