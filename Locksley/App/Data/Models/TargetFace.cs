using Locksley.App.Data.Models.Interfaces;
using Locksley.App.Helpers.Enums;
using Microsoft.EntityFrameworkCore;

namespace Locksley.App.Data.Models;

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public record TargetFace : IHasRelationship {
    public int TargetFaceId { get; set; }

    public int Size { get; set; }

    public LengthUnits SizeUnits { get; set; } = LengthUnits.Centimeters;

    public virtual ICollection<ScoreZone> ScoreZones { get; set; } = new List<ScoreZone>();
    
    public static void ConfigureRelationships(ModelBuilder modelBuilder) {
        modelBuilder.Entity<TargetFace>()
            .HasMany(tf => tf.ScoreZones)
            .WithMany()
            .UsingEntity("TargetFace_ScoreZones");
    }
}