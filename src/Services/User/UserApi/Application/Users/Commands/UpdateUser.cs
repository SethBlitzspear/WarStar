using Application.Common.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Users.Commands;
public sealed record UpdateUserCommand : IRequest<UserDto>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public sealed class UpdateUser(IApplicationDbContext context, IMapper mapper) : IRequestHandler<UpdateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Users.FindAsync(request.Id, cancellationToken);

        if (entity != null)
        {
            entity.Name = request.Name;
            entity.Email = request.Email;
            entity.Username = request.Username;
            entity.Password = request.Password;

            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<UserDto>(entity);
        }

        throw new EntityNotFoundException($"Unable to find User with ID of {request.Id}");
    }
}