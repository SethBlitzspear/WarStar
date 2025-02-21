using Api.Infrastructure;
using Application.Common.Dtos;
using Application.Users.Commands;
using Application.Users.Queries;
using MediatR;

namespace Api.Endpoints;

public sealed class User : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateUser)
            .MapGet(GetUser, "{id:guid}")
            .MapGet(GetUserByUserName, "{userName}")
            .MapGet(GetUsers)
            .MapPut(UpdateUser, "{id}")
        .MapDelete(DeleteUser, "{id}");
    }

    private static async Task<Guid> CreateUser(ISender sender, UserDto command, CancellationToken cancellationToken)
    {
        return await sender.Send(new CreateUserCommand
        {
            Name = command.Name,
            Email = command.Email,
            Username = command.Username,
            Password = command.Password
        }, cancellationToken);
    }

    private static async Task<UserDto> GetUser(ISender sender, Guid id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetUserQuery(id), cancellationToken);
    }
    private static async Task<UserDto> GetUserByUserName(ISender sender, string userName, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetUserByUserNameQuery(userName), cancellationToken);
    }

    private static async Task<List<UserDto>> GetUsers(ISender sender, CancellationToken cancellationToken)
    {
      return await sender.Send(new GetUsersQuery(), cancellationToken);
    }

    private static async Task<UserDto> UpdateUser(ISender sender, Guid id, UserDto command, CancellationToken cancellationToken)
    {
      return await sender.Send(new UpdateUserCommand
      {
          Id = id,
          Name = command.Name,
          Email = command.Email,
          Username = command.Username,
          Password = command.Password
      }, cancellationToken);
    }

    private static async Task<bool> DeleteUser(ISender sender, Guid id)
    {
       return await sender.Send(new DeleteUserCommand(id));
    }
}
