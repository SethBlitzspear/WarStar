using Application.Commands.CreateUser;
using AutoMapper;
using Domain.Entities;

namespace Application.Dtos;
public class CreateUserDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public sealed class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<User, CreateUserDto>()
            .ReverseMap();
        CreateMap<CreateUserCommand, CreateUserDto>()
            .ReverseMap();
    }
}