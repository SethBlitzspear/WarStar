using Application.Shipyard.Commands.CreateSpaceship;
using AutoMapper;
using Domain.Entities;

namespace Application.Dtos;
public class CreateSpaceShipDto
{
    public required string Name { get; set; }
    public Guid OwnerId { get; set; }
    public sealed class CreateSpaceShipDtoMappings : Profile
    {
        public CreateSpaceShipDtoMappings()
        {
            CreateMap<SpaceShip, CreateSpaceShipDto>()
                .ReverseMap();
            CreateMap<CreateSpaceShipCommand, CreateSpaceShipDto>()
               .ReverseMap();
        }
    }
}

