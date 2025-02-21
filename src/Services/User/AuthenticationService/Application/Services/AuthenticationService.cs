using Application.Dtos;
using Application.Interfaces;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;
public class AuthenticationService(IUserRepository userRepository, IMapper mapper) : IAuthenticationService
{
    public async Task<Guid> RegisterUser(CreateUserDto userDto)
    {
        // Hash the password before saving it
        var hashedPassword = HashPassword(userDto.Password);

        var user = mapper.Map<User>(userDto);
        user.Password = hashedPassword;

       return await userRepository.CreateUser(user);
    }
    
    public async Task<LoginResponseDto> Login(LoginRequestDto request)
    {
        var user = await userRepository.GetUserByUsername(request.Username);
        if (user == null || !ValidatePassword(request.Password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        // Generate JWT or other token (if applicable)
        var token = GenerateToken(user);

        return new LoginResponseDto
        {
            Token = token,
            UserId = user.Id,
            EMail = user.Email,
            Name = user.Name // Added to satisfy the required member 'Name'
        };
    }

    public static bool ValidatePassword(string plainPassword, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(plainPassword, storedHash);
    }

    private static string GenerateToken(User user)
    {
        // Logic for generating a JWT or session token.
        return "generated-token";
    }

    private static string HashPassword(string plainPassword)
    {
        // Use a library like BCrypt.Net-Next or similar to hash the password
        return BCrypt.Net.BCrypt.HashPassword(plainPassword);
    }
}