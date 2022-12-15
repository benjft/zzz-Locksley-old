using Locksley.App.Helpers.Enums;

namespace Locksley.App.Models;

public record TargetFace {
    public int TargetFaceId { get; set; }

    public int Size { get; set; }

    public LengthUnits SizeUnits { get; set; } = LengthUnits.Centimeters;

    public virtual ICollection<ScoreZone> ScoreZones { get; set; } = new List<ScoreZone>();
}