using Application.Dtos;
using Application.Interfaces;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CreateUser;

public record CreateUserCommand() : IRequest<Guid>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class CreateUser(IAuthenticationService authenticationService, IMapper mapper) : IRequestHandler<CreateUserCommand, Guid>
{

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await authenticationService.RegisterUser(mapper.Map<CreateUserDto>(request));
    }
}
