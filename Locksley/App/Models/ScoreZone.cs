namespace Locksley.App.Models;

public record ScoreZone {
    public int ScoreZoneId { get; set; }

    public int Score { get; set; }

    public double Radius { get; set; }
}