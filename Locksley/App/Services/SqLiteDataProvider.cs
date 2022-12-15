using Locksley.App.Models;
using Locksley.App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Locksley.App.Services;

public sealed class SqLiteDataProvider : DbContext, IDataProvider {

    public DbSet<ScoreSheet> ScoreSheets { get; set; }

    private readonly ILogger _log;
    
    public SqLiteDataProvider(ILogger log) {
#if IOS
        SQLitePCL.Batteries_V2.Init(); // initializes sqlite on iOS
#endif
        Database.EnsureCreated();
        _log = log;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlite($"Filename={Constants.DatabasePath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        _log.LogInformation($"Creating fk for ScoreSheet");
        modelBuilder.Entity<ScoreSheet>()
            .HasMany(ss => ss.Rounds)
            .WithOne();
        
        _log.LogInformation("Creating fk for Round");
        modelBuilder.Entity<Round>()
            .HasOne(r => r.TargetFace)
            .WithMany();
        modelBuilder.Entity<Round>()
            .HasMany(r => r.Ends)
            .WithOne();

        _log.LogInformation("Creating fk for TargetFace");
        modelBuilder.Entity<TargetFace>()
            .HasMany(tf => tf.ScoreZones)
            .WithMany()
            .UsingEntity("TargetFace_ScoreZones");

        _log.LogInformation("Creating fk for End");
        modelBuilder.Entity<End>()
            .HasMany(e => e.Scores)
            .WithOne();
    }

    public IEnumerable<ScoreSheet> GetAllScoreSheets() => ScoreSheets;

    public void LoadScoreSheet(ref ScoreSheet scoreSheet) {
        Entry(scoreSheet).Collection(ss => ss.Rounds).Load();
    }
}