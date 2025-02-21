using Application.Dtos;
using Application.Interfaces;
using Application.Services;
using MediatR;

namespace Application.Commands.AuthenticateUser;

public class AuthenticateUserCommand() : IRequest<LoginResponseDto>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class AuthenticateUser(IAuthenticationService authenticationService) : IRequestHandler<AuthenticateUserCommand, LoginResponseDto>
{

    public async Task<LoginResponseDto> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        // Use the AuthenticationService to handle login logic
        var result = await authenticationService.Login(new LoginRequestDto
        {
            Username = request.Username,
            Password = request.Password
        });

        return result;
    }
}