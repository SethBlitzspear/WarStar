using AutoMapper;
using Domain.Entities;

namespace Application.Common.Dtos;

public class ComponentDto
{
    public Guid? Id { get; set; }

    public Guid SpaceShipId { get; set; }
    public Guid ComponentTypeId { get; set; }

    public int Armour { get; set; }
    public int StructuralIntegrity { get; set; }
    public int MinPowerDraw { get; set; }
    public int MaxPowerDraw { get; set; }
    public bool LifeSupport { get; set; }
    public int Mass { get; set; }
    public double Price { get; set; }
    public string? Properties { get; set; }

    public Guid? TopComponentId { get; set; }
    public Guid? BottomComponentId { get; set; }
    public Guid? LeftComponentId { get; set; }
    public Guid? RightComponentId { get; set; }

    public int Connections { get; set; }
    public int PowerCouplings { get; set; }

    private sealed class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Component, ComponentDto>()
                .ReverseMap();
        }
    }
}
