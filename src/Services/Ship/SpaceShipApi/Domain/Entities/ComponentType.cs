using System.Security.Cryptography.X509Certificates;

namespace Domain.Entities;

public class ComponentType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Armour { get; set;}
    public int StructuralIntegrity { get; set; }
    public int MinPowerDraw { get; set; }
    public int MaxPowerDraw { get; set; }
    public bool LifeSupport { get; set; }
    public int Mass { get; set; }
    public double Price { get; set; }
    public string? Properties { get; set; }

}
