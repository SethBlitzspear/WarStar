using AutoMapper;
using Domain.Entities;
using Domain.Entities.Components;
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
            CreateMap<ComponentDto, Component>()
                .ConvertUsing<ComponentTypeConverter>(); // Use a custom converter for subclass mapping

            CreateMap<Component, ComponentDto>();
        }
    }

    public class ComponentTypeConverter : ITypeConverter<ComponentDto, Component>
    {
        public Component Convert(ComponentDto source, Component destination, ResolutionContext context)
        {
            var componentTypeList = (List<ComponentType>)context.Items["ComponentTypeLookup"];
            var componentType = componentTypeList.FirstOrDefault(x => x.Id == source.ComponentTypeId);

            Component mappedComponent = componentType.Type switch
            {
                "Laser" => new Laser(),
                "Keel" => new Keel(),
                "Reactor" => new Reactor(),
                "Engine" => new Engine(),
                _ => new Component()
            };

            // 🚀 **Manually map properties from ComponentDto to Component**
            mappedComponent.Id = source.Id;
            mappedComponent.SpaceShipId = source.SpaceShipId;
            mappedComponent.ComponentTypeId = source.ComponentTypeId;
            mappedComponent.Armour = source.Armour;
            mappedComponent.StructuralIntegrity = source.StructuralIntegrity;
            mappedComponent.MinPowerDraw = source.MinPowerDraw;
            mappedComponent.MaxPowerDraw = source.MaxPowerDraw;
            mappedComponent.LifeSupport = source.LifeSupport;
            mappedComponent.Mass = source.Mass;
            mappedComponent.Price = source.Price;
            mappedComponent.Properties = source.Properties;

            // Restore ComponentType reference
            mappedComponent.ComponentType = componentType;

            // Restore connections to other components (but do not map recursively!)
            mappedComponent.TopComponentId = source.TopComponentId;
            mappedComponent.BottomComponentId = source.BottomComponentId;
            mappedComponent.LeftComponentId = source.LeftComponentId;
            mappedComponent.RightComponentId = source.RightComponentId;

            // Do NOT map the actual component references (TopComponent, BottomComponent, etc.)
            // because that would trigger recursion. These should be resolved in a separate step.

            mappedComponent.Connections = source.Connections;
            mappedComponent.PowerCouplings = source.PowerCouplings;

            return mappedComponent;
        }
    }
}