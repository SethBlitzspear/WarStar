using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class ShipLayoutService : IShipLayoutService
{
    public Component[][] BuildShipLayout(List<Component> components)
    {
        List<ComponentPosition> mappedComponents = [];
        var keel = components.Find(c => c.ComponentType.Type.Equals("Keel", StringComparison.OrdinalIgnoreCase));
        keel?.MapComponents(0, 0, mappedComponents);

        var minX = mappedComponents.Min(mc => mc.X);
        var maxX = mappedComponents.Max(mc => mc.X);
        var minY = mappedComponents.Min(mc => mc.Y);
        var maxY = mappedComponents.Max(mc => mc.Y);

        var difX = maxX - minX + 1;
        var difY = maxY - minY + 1;

        var shipGrid = new Component[difY][];
        for (int i = 0; i < difY; i++)
        {
            shipGrid[i] = new Component[difX];
        }

        var midx = 0 - minX;
        var midy = 0 - minY;

        foreach (var mappedComponent in mappedComponents)
        {
            shipGrid[midy + mappedComponent.Y][midx + mappedComponent.X] = mappedComponent.Component;
        }

        return shipGrid;
    }
}
