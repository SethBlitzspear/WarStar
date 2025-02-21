using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Users.Commands;

public sealed record DeleteUserCommand(Guid Id) : IRequest<bool>;

public sealed class DeleteUser(IApplicationDbContext context) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await context.Users.FindAsync(request.Id, cancellationToken) ??
                         throw new EntityNotFoundException($"Unable to find User with ID of {request.Id}");
            context.Users.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}  