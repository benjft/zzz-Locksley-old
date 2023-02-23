using Locksley.App.Data.Models.Interfaces;
using Locksley.App.Helpers.Enums;
using Microsoft.EntityFrameworkCore;

namespace Locksley.App.Data.Models;

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public record Section : IHasRelationship {
    public int SectionId { get; set; }

    public int RoundNumber { get; set; }

    public int Distance { get; set; }

    public LengthUnits DistanceUnits { get; set; } = LengthUnits.Meters;

    public virtual TargetFace TargetFace { get; set; } = default!;

    public virtual ICollection<End> Ends { get; set; } = new List<End>();
    public static void ConfigureRelationships(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Section>()
            .HasOne(r => r.TargetFace)
            .WithMany();
        
        modelBuilder.Entity<Section>()
            .HasMany(r => r.Ends)
            .WithOne();
    }
}