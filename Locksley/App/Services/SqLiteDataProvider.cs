using System.Xml.Linq;
using Locksley.App.Attributes;
using Locksley.App.Converters;
using Locksley.App.Helpers;
using Locksley.App.Models;
using Locksley.App.Models.Interfaces;
using Locksley.App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

namespace Locksley.App.Services;

[ServiceLifetime(Lifetime = ServiceLifetime.Singleton)]
public sealed class SqLiteDataProvider : DbContext, IDataProvider {

    public DbSet<ScoreSheet> ScoreSheets { get; set; }

    private readonly ILogger _log;
    
    public SqLiteDataProvider(ILogger<SqLiteDataProvider> log) {
        _log = log;
#if IOS
        SQLitePCL.Batteries_V2.Init(); // initializes sqlite on iOS
#endif
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlite($"Filename={Constants.DatabasePath}");
    }

    private void RegisterRelationships(ModelBuilder modelBuilder) {
        var modelNamespace = typeof(ScoreSheet).Namespace;
        var relationshipTypes = ReflectionHelper.GetAllClassesInNamespace(modelNamespace)
            .Where(t => typeof(IHasRelationship).IsAssignableFrom(t));
        
        foreach (var type in relationshipTypes) {
            _log.LogDebug("Running \"ConfigureRelationships\" for type \"{Name}\"", type.Name);
            type.GetMethod(nameof(IHasRelationship.ConfigureRelationships))?.Invoke(null, new object[] {modelBuilder});
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        RegisterRelationships(modelBuilder);
    }

    private void RegisterConverters(ModelConfigurationBuilder configurationBuilder) {
        var converterNamespace = typeof(XElementConverter).Namespace;
        var valueConverters = ReflectionHelper.GetAllSubclassesInNamespace<ValueConverter>(converterNamespace);
        
        foreach (var converterType in valueConverters) {
            var targetType = converterType?.BaseType?.GetGenericArguments()[0] ?? throw new NullReferenceException();
            
            _log.LogDebug("Adding converter \"{Converter}\" for type \"{Target}\"", converterType.Name, targetType.Name);
            configurationBuilder.Properties(targetType).HaveConversion(converterType);
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
        RegisterConverters(configurationBuilder);
    }

    public IEnumerable<ScoreSheet> GetAllScoreSheets() => ScoreSheets;

    public void LoadScoreSheet(ref ScoreSheet scoreSheet) {
        Entry(scoreSheet).Collection(ss => ss.Sections).Load();
    }
}