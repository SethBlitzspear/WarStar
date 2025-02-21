using Api.Infrastructure;
using Application.Commands.AuthenticateUser;
using Application.Commands.CreateUser;
using Application.Dtos;
using AutoMapper;
using MediatR;

namespace API.Endpoints;

public sealed class User : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateUser, "Create")
            .MapPost(AuthenticateUser);
    }

    private static async Task<Guid> CreateUser(ISender sender, CreateUserDto userDto, CancellationToken cancellationToken, IMapper mapper)
    {
        return await sender.Send(mapper.Map<CreateUserCommand>(userDto), cancellationToken);
    }

    private static async Task<LoginResponseDto> AuthenticateUser(ISender sender, LoginRequestDto request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new AuthenticateUserCommand
            {
                Username = request.Username,
                Password = request.Password
            }, cancellationToken);
    }
}