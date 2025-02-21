using Domain.Enums;

namespace Domain.Entities;

public sealed class Component
{
    public Guid Id { get; set; }

    public Guid ComponentTypeId { get; set; }
    public ComponentType ComponentType { get; set; } = null!;

    public int Armour { get; set; }
    public int StructuralIntegrity { get; set; }
    public int MinPowerDraw { get; set; }
    public int MaxPowerDraw { get; set; }
    public bool LifeSupport { get; set; }
    public int Mass { get; set; } 
    public double Price { get; set; }
    public string? Properties { get; set; }

    public Guid SpaceShipId { get; set; }
    public SpaceShip SpaceShip { get; set; } = null!;

    public Guid? TopComponentId { get; set; }
    public Component? TopComponent { get; set; }
    public Guid? BottomComponentId { get; set; }
    public Component? BottomComponent { get; set; }

    public Guid? LeftComponentId { get; set; }
    public Component? LeftComponent { get; set; }
    public Guid? RightComponentId { get; set; }
    public Component? RightComponent { get; set; }

    public ConnectionType Connections { get; set; }
    public ConnectionType PowerCouplings { get; set; }
}