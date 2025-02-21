namespace Domain.Entities;

public sealed class SpaceShip
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid OwnerId { get; set; }
    public List<Component>? Components { get; set; } 
}
