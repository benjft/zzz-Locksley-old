using Locksley.App.Helpers.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Locksley.App.Models;

[Table(nameof(TargetFace))]
public record TargetFace {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    public int Size { get; set; }

    public LengthUnits SizeUnits { get; set; } = LengthUnits.Centimeters;

    [TextBlob(nameof(ScoreZonesBlob))]
    public List<int> ScoreZones { get; set; } = new();

    public double SizeFraction { get; set; } = 1;
    
    public string ScoreZonesBlob { get; set; } = "";
}