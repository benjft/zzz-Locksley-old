using Locksley.App.Data.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Locksley.App.Data.Models;

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public record ScoreSheet : IHasRelationship {
    public int ScoreSheetId { get; set; }

    public string Title { get; set; } = "";

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
    
    public static void ConfigureRelationships(ModelBuilder modelBuilder) {
        modelBuilder.Entity<ScoreSheet>()
            .HasMany(ss => ss.Sections)
            .WithOne();
    }
}