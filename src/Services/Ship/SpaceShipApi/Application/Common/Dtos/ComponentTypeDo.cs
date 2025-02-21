using AutoMapper;
using Domain.Entities;

namespace Application.Common.Dtos;

public class ComponentTypeDto
{
    public Guid? Id { get; set; }

    public required string Type { get; set; }
    public required string Name { get; set; }
    public required string ShortName { get; set; }
    public int Armour { get; set; }
    public int StructuralIntegrity { get; set; }
    public int MinPowerDraw { get; set; }
    public int MaxPowerDraw { get; set; }
    public bool LifeSupport { get; set; }
    public int Mass { get; set; }
    public double Price { get; set; }
    public string? Properties { get; set; }

    private sealed class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<ComponentType, ComponentTypeDto>();
        }
    }
}