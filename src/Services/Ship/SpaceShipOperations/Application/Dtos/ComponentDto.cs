using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.Dtos;

public class ComponentDto
{
    public Guid Id { get; set; }

    public Guid SpaceShipId { get; set; }
    public Guid ComponentTypeId { get; set; }

    public ComponentTypeDto ComponentType { get; set; } = null!;

    public int Armour { get; set; }
    public int StructuralIntegrity { get; set; }
    public int MinPowerDraw { get; set; }
    public int MaxPowerDraw { get; set; }
    public bool LifeSupport { get; set; }
    public int Mass { get; set; }
    public double Price { get; set; }
    public string? Properties { get; set; }

    public Guid? TopComponentId { get; set; }
    [JsonIgnore]
    public ComponentDto? TopComponent { get; set; }
    public Guid? BottomComponentId { get; set; }
    [JsonIgnore]
    public ComponentDto? BottomComponent { get; set; }

    public Guid? LeftComponentId { get; set; }
    [JsonIgnore]
    public ComponentDto? LeftComponent { get; set; }
    public Guid? RightComponentId { get; set; }
    [JsonIgnore]
    public ComponentDto? RightComponent { get; set; }

    public ConnectionType Connections { get; set; }
    public ConnectionType PowerCouplings { get; set; }

    public sealed class ComponentMappings : Profile
    {
        public ComponentMappings()
        {
            CreateMap<Component, ComponentDto>()
                .ReverseMap();
        }
    }
}