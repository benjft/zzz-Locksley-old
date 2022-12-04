using Locksley.App.Helpers.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Locksley.App.Models;

[SQLite.Table(nameof(Round))]
public record Round {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    [ForeignKey(typeof(ScoreSheet))]
    public int ScoreSheetId { get; set; }

    public int RoundNumber { get; set; }
    
    public int Distance { get; set; }

    public LengthUnits DistanceUnits { get; set; } = LengthUnits.Meters;
    
    [ForeignKey(typeof(TargetFace))]
    public int? TargetFaceId { get; set; }

    [ManyToOne]
    public TargetFace? TargetFace { get; set; } = default!;

    [OneToMany]
    public List<End> Ends { get; set; } = new();
}