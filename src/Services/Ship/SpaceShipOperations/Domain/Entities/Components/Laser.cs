namespace Domain.Entities.Components;

public class Laser : Component
{
    public int Damage => ExtendedProperties?.TryGetValue("Damage", out object? value) == true ? Convert.ToInt32(value) : 0;
    public int EffectiveRange => ExtendedProperties?.TryGetValue("EffectiveRange", out object? value) == true ? Convert.ToInt32(value) : 0;
    public double FallOffRate => ExtendedProperties?.TryGetValue("FallOffRate", out object? value) == true ? Convert.ToDouble(value) : 0.0;
}