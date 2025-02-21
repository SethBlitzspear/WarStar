using AutoMapper;
using Domain.Entities;

namespace Application.Dtos;
public class SpaceShipDto
{
    public required string Name { get; set; }
    public required Guid Id { get; set; }
    public Guid OwnerId { get; set; }

    public sealed class SpaceShipMappings : Profile
    {
        public SpaceShipMappings()
        {
            CreateMap<SpaceShip, SpaceShipDto>()
                .ReverseMap();
        }
    }
}

