using Locksley.App.Helpers.Enums;

namespace Locksley.App.Models;

public record Round {
    public int RoundId { get; set; }

    public int RoundNumber { get; set; }

    public int Distance { get; set; }

    public LengthUnits DistanceUnits { get; set; } = LengthUnits.Meters;

    public TargetFace TargetFace { get; set; } = default!;

    public virtual ICollection<End> Ends { get; set; } = new List<End>();
}