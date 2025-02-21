using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands;

public record CreateUserCommand() : IRequest<Guid>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class CreateUser(IApplicationDbContext context) : IRequestHandler<CreateUserCommand, Guid>
{

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new User
        {
            Name = request.Name,
            Email = request.Email,
            Username = request.Username,
            Password = request.Password
        };

       await context.Users.AddAsync(entity, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return entity.Id; 
    }
}
