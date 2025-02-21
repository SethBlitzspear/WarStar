using Domain.Entities;

namespace Application.Interfaces;

public interface IShipLayoutService
{
    public Component[][] BuildShipLayout(List<Component> components);
}
