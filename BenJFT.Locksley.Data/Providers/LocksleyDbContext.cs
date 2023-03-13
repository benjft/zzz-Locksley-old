using System.Xml.Linq;
using BenJFT.Locksley.Data.Converters;
using BenJFT.Locksley.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
#if IOS
using SQLitePCL;
#endif

namespace BenJFT.Locksley.Data.Providers;

public sealed class LocksleyDbContext : DbContext {
    private readonly ILogger<LocksleyDbContext>? _log;

    public LocksleyDbContext(
        DbContextOptions<LocksleyDbContext> contextOptions,
        ILogger<LocksleyDbContext>? log
    ) : base(contextOptions) {
        _log = log;

#if IOS
        Batteries_V2.Init(); // initializes sqlite on iOS
#endif

        Database.EnsureCreated();
    }

    public DbSet<ScoreSheet> ScoreSheets { get; set; }
    public DbSet<TargetFace> TargetFaces { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
        _log?.LogInformation("Adding conversions...");

        AddConversion<XElement, XElementConverter>(configurationBuilder);
    }

    private void AddConversion<TProperty, TConverter>(ModelConfigurationBuilder configurationBuilder) {
        _log?.LogInformation(
            "Adding Converter '{TConverter}' for '{TProperty}'",
            typeof(TConverter).Name,
            typeof(TProperty).Name);

        configurationBuilder.Properties<TProperty>().HaveConversion<TConverter>();
    }
}