using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Configurations;
public class ComponentConfiguration : IEntityTypeConfiguration<Component>
{
    public void Configure(EntityTypeBuilder<Component> builder)
    {
        // One-to-One relationships for the Component entity
        builder.HasOne(c => c.TopComponent)
            .WithOne()
            .HasForeignKey<Component>(c => c.TopComponentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.BottomComponent)
            .WithOne()
            .HasForeignKey<Component>(c => c.BottomComponentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.LeftComponent)
            .WithOne()
            .HasForeignKey<Component>(c => c.LeftComponentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.RightComponent)
            .WithOne()
            .HasForeignKey<Component>(c => c.RightComponentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relationship between Component and SpaceShip
        builder.HasOne(c => c.SpaceShip)
            .WithMany(s => s.Components) // Assuming SpaceShip has a collection of Components
            .HasForeignKey(c => c.SpaceShipId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete to remove Components when SpaceShip is deleted
    }
}
