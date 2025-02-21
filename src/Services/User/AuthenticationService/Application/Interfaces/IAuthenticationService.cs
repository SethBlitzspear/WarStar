using Application.Dtos;
using Application.Services;

namespace Application.Interfaces;

public interface IAuthenticationService
{
    public Task<LoginResponseDto> Login(LoginRequestDto request);
    public Task<Guid> RegisterUser(CreateUserDto userDto);
}