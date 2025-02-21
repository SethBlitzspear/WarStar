using Application.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Application.Common.Interfaces;
using Domain.Enums;

namespace Infrastructure.Data;
public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<SpaceShip> SpaceShips => Set<SpaceShip>();
    public DbSet<Component> Components => Set<Component>();
    public DbSet<ComponentType> ComponentTypes => Set<ComponentType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);

        var keelTypeId = GuidPool.GetGuid();
        var engineTypeId = GuidPool.GetGuid();
        var reactorTypeId = GuidPool.GetGuid();
        var weaponTypeId = GuidPool.GetGuid();

        var componentSeedData = new List<ComponentType>
        {
            new ComponentType { Id = keelTypeId, Type = "Keel", Name = "Keelster 1000", ShortName = "K1000", Armour = 10, StructuralIntegrity = 100, LifeSupport = false, MinPowerDraw = 0, MaxPowerDraw = 0, Mass = 1000, Price = 12000.00 },
            new ComponentType { Id = engineTypeId, Type = "Engine", Name = "ThrustMaster Max", ShortName = "T-Max", Armour = 2, StructuralIntegrity = 30, LifeSupport = true, MinPowerDraw = 10, MaxPowerDraw = 1000, Mass = 200, Price = 5000.00 },
            new ComponentType { Id = reactorTypeId, Type = "Reactor", Name = "PowerCore Fusion Pro", ShortName = "PC-FP", Armour = 1, StructuralIntegrity = 10, LifeSupport = true, MinPowerDraw = 0, MaxPowerDraw = 1, Mass = 250, Price = 7000.00},
            new ComponentType { Id = weaponTypeId, Type = "Weapon", Name = "Type 4 Beam Laser", ShortName = "BL-4", Armour = 2, StructuralIntegrity = 20, LifeSupport = false, MinPowerDraw = 0, MaxPowerDraw = 100, Mass = 50, Price = 2000.00 }
        };

        modelBuilder.Entity<ComponentType>().HasData(componentSeedData);
    } 
}

public static class GuidPool
{
    public static List<Guid> ComponentTypeGuids = new()
    {
        Guid.Parse("e0a27785-fa7a-48f2-a8ec-67800194fe7c"),
        Guid.Parse("6fa019bb-59be-43a9-9b00-9f7a491ac59b"),
        Guid.Parse("d6eaf71c-eb7e-4f4b-870f-ef67bb09c035"),
        Guid.Parse("a5e4cac9-b9fa-4982-8134-c19932427cc2"),
        Guid.Parse("96ea7006-3195-4ebb-882a-1823858bb161"),
        Guid.Parse("a46766a0-0a4f-4314-a2b8-0af36163fec9"),
        Guid.Parse("06cea7a9-90ef-4cfe-be6d-fe6be63583d1"),
        Guid.Parse("89abef19-f35d-4aa7-ba0d-fff02967069c"),
        Guid.Parse("52fdd830-e46b-4553-80a5-8593604b5a94"),
        Guid.Parse("901a02b2-f034-4c6e-90d0-63c5b9adfbe5"),
        Guid.Parse("2e819bc1-335d-47db-9026-b37c979fe99b"),
        Guid.Parse("48a8e98f-8401-47e4-8350-1cd97c11c8eb"),
        Guid.Parse("a387a266-bea0-4258-bca2-20eb4c79dc54"),
        Guid.Parse("7436c72e-d9f0-4c02-a8c5-f89670034193"),
        Guid.Parse("db76ae7f-72ae-4a78-a5d6-639f47176a2c"),
        Guid.Parse("f1e61823-e94b-4a13-9630-b0d99545bea9"),
        Guid.Parse("37576082-19ae-4e56-8fcf-6c6be98484db"),
        Guid.Parse("3e559636-0eb7-4f66-b2f5-e62af79f7152"),
        Guid.Parse("ec4b4a70-3877-44e0-b05c-a259275b22c0"),
        Guid.Parse("192243ca-04dc-4be8-9615-ae326e86dc78"),
        Guid.Parse("ebf3d4be-4489-40a4-ac64-2503fa3c49e5"),
        Guid.Parse("783a8cf6-def4-438f-abd8-e80b46915c37"),
        Guid.Parse("260cb7b4-3db9-4806-b2cc-95507739a549"),
        Guid.Parse("be850821-6ac2-4071-badb-9ea9b62fb332"),
        Guid.Parse("de186360-6988-46e6-8531-e291bb693fd9"),
        Guid.Parse("09639fbd-5358-4f26-9d53-bd135a8afcfd"),
        Guid.Parse("f55be0e3-477f-48d7-ac0c-24bfa627284d"),
        Guid.Parse("b7784fbd-a861-4369-90e5-bd8e89fea56a"),
        Guid.Parse("907fa4b0-216d-498f-a68d-64d1f4f252b7"),
        Guid.Parse("5845879d-40e4-47d8-b3d8-ce5096eaa7eb")
    };

    internal static Guid GetGuid()
    {
        var guid = ComponentTypeGuids.FirstOrDefault(Guid.NewGuid());
        ComponentTypeGuids.Remove(guid);
        return guid;
    }
}