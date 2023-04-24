using System.Xml.Linq;

namespace BenJFT.Locksley.Data.Models;

public record ScoreZone {
    public int ScoreZoneId { get; set; }

    public int Score { get; set; }

    public double Radius { get; set; }

    public XElement Geometry { get; set; } = new("");
}