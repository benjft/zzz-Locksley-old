using Locksley.App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Locksley.App.Models;

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public record End : IHasRelationship {
    public int EndId { get; set; }

    public virtual ICollection<ArrowValue> Scores { get; set; } = new List<ArrowValue>();
    
    public static void ConfigureRelationships(ModelBuilder modelBuilder) {
        modelBuilder.Entity<End>()
            .HasMany(e => e.Scores)
            .WithOne();
    }
}