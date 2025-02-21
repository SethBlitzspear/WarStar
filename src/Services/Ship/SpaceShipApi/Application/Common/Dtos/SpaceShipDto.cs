using AutoMapper;
using Domain.Entities;

namespace Application.Common.Dtos;
public class SpaceShipDto
{
    public Guid? Id { get; set; }
    public required string Name { get; set; }
    public Guid OwnerId { get; set; }

    private sealed class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<SpaceShip, SpaceShipDto>();
        }
    }
}
