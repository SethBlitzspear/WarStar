using AutoMapper;
using Domain.Entities;

namespace Application.Dtos;

public class SpaceShipDetailDto : SpaceShipDto
{
    public ComponentDto[][]? ShipLayout { get; set; }
    public int Mass { get; set; }
    public int BackgroundPowerDraw { get; set; }
    public int ShipSize { get; set; }

    public sealed class SpaceShipDetailMappings : Profile
    {
        public SpaceShipDetailMappings()
        {
            CreateMap<SpaceShip, SpaceShipDetailDto>()
                .ReverseMap();
            CreateMap<SpaceShipDto, SpaceShipDetailDto>()
                .ReverseMap();
        }
    }
}