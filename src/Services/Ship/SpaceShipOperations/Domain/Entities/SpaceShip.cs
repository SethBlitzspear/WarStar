namespace Domain.Entities;

public sealed class SpaceShip
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid OwnerId { get; set; }
    public List<Component>? Components { get; set; }

    public int Mass => Components?.Sum(c => c.Mass) ?? 0;
    public int BackgroundPowerDraw => Components?.Sum(c => c.MinPowerDraw) ?? 0;
}
