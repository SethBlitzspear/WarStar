using Domain.Enums;
using System.Text.Json;

namespace Domain.Entities;

public class Component
{
    private Dictionary<string, object>? _extendedProperties;
    private string? _properties;

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
    public string? Properties
    {
        get => _properties;
        set
        {
            _properties = value;
            _extendedProperties = null;
        }
    }

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

    public bool UpConnection
    {
        get => Connections.HasFlag(ConnectionType.Up);
        set => Connections = value ? Connections | ConnectionType.Up : Connections & ~ConnectionType.Up;
    }

    public bool DownConnection
    {
        get => Connections.HasFlag(ConnectionType.Down);
        set => Connections = value ? Connections | ConnectionType.Down : Connections & ~ConnectionType.Down;
    }

    public bool LeftConnection
    {
        get => Connections.HasFlag(ConnectionType.Left);
        set => Connections = value ? Connections | ConnectionType.Left : Connections & ~ConnectionType.Left;
    }

    public bool RightConnection
    {
        get => Connections.HasFlag(ConnectionType.Right);
        set => Connections = value ? Connections | ConnectionType.Right : Connections & ~ConnectionType.Right;
    }

    public bool UpPowerCoupling
    {
        get => PowerCouplings.HasFlag(ConnectionType.Up);
        set => PowerCouplings = value ? PowerCouplings | ConnectionType.Up : PowerCouplings & ~ConnectionType.Up;
    }

    public bool DownPowerCoupling
    {
        get => PowerCouplings.HasFlag(ConnectionType.Down);
        set => PowerCouplings = value ? PowerCouplings | ConnectionType.Down : PowerCouplings & ~ConnectionType.Down;
    }

    public bool LeftPowerCoupling
    {
        get => PowerCouplings.HasFlag(ConnectionType.Left);
        set => PowerCouplings = value ? PowerCouplings | ConnectionType.Left : PowerCouplings & ~ConnectionType.Left;
    }

    public bool RightPowerCoupling
    {
        get => PowerCouplings.HasFlag(ConnectionType.Right);
        set => PowerCouplings = value ? PowerCouplings | ConnectionType.Right : PowerCouplings & ~ConnectionType.Right;
    }

    public Dictionary<string, object>? ExtendedProperties
    {
        get
        {
            if (_extendedProperties == null && !string.IsNullOrEmpty(Properties))
            {
                try
                {
                    _extendedProperties = JsonSerializer.Deserialize<Dictionary<string, object>>(Properties) ?? null;
                }
                catch
                {
                    return null;
                }
            }
            return _extendedProperties;
        }
    }

    public void MapComponents(int x, int y, List<ComponentPosition> mappedComponents)
    {
        if (mappedComponents.Any(c => c.Component.Equals(this)))
        {
            return;
        }
        mappedComponents.Add(new ComponentPosition()
        {
            Component = this,
            X = x,
            Y = y
        });

        LeftComponent?.MapComponents(x - 1, y, mappedComponents);
        RightComponent?.MapComponents(x + 1, y, mappedComponents);
        TopComponent?.MapComponents(x, y - 1, mappedComponents);
        BottomComponent?.MapComponents(x, y + 1, mappedComponents);
    }
}

public class ComponentPosition
{
    public required Component Component { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}