using AutoMapper;
using Domain.Entities;

namespace Application.Common.Dtos;
public class UserDto
{
    public Guid? Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }

    private sealed class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<User, UserDto>();
        }
    }
}
