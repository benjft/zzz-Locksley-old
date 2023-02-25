using Locksley.App.Attributes;
using Locksley.App.Converters;
using Locksley.App.Data.Models;
using Locksley.App.Data.Models.Interfaces;
using Locksley.App.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

namespace Locksley.App.Data;

[ServiceLifetime(Lifetime = ServiceLifetime.Singleton)]
public sealed class LocksleyDbContext : DbContext {

    private readonly ILogger _log;

    public LocksleyDbContext(
        DbContextOptions<LocksleyDbContext> contextOptions, 
        ILogger<LocksleyDbContext> log
    ) : base(contextOptions) {
        _log = log;
        
        #if IOS
        SQLitePCL.Batteries_V2.Init(); // initializes sqlite on iOS
        #endif
        Database.EnsureCreated();
    }

    #region public properties and methods

    public DbSet<ScoreSheet> ScoreSheets { get; set; }
    public DbSet<TargetFace> TargetFaces { get; set; }

    #endregion

    #region base class overrides
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlite($"Filename={Constants.DatabasePath}");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        RegisterRelationships(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
        RegisterConverters(configurationBuilder);
    }

    #endregion
    #region private helpers
    private void RegisterRelationships(ModelBuilder modelBuilder) {
        var modelNamespace = typeof(ScoreSheet).Namespace;
        var relationshipTypes = ReflectionHelper.GetAllClassesInNamespace(modelNamespace)
            .Where(t => typeof(IHasRelationship).IsAssignableFrom(t));
        
        foreach (var type in relationshipTypes) {
            _log.LogDebug("Running \"ConfigureRelationships\" for type \"{Name}\"", type.Name);
            type.GetMethod(nameof(IHasRelationship.ConfigureRelationships))?.Invoke(null, new object[] {modelBuilder});
        }
    }

    private void RegisterConverters(ModelConfigurationBuilder configurationBuilder) {
        var converterNamespace = typeof(XElementConverter).Namespace;
        var valueConverters = ReflectionHelper.GetAllSubclassesInNamespace<ValueConverter>(converterNamespace);
        
        foreach (var converterType in valueConverters) {
            var targetType = converterType.BaseType?.GetGenericArguments()[0] ?? throw new NullReferenceException();
            
            _log.LogDebug("Adding converter \"{Converter}\" for type \"{Target}\"", converterType.Name, targetType.Name);
            configurationBuilder.Properties(targetType).HaveConversion(converterType);
        }
    }
    #endregion
}