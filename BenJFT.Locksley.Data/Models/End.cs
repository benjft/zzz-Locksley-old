namespace BenJFT.Locksley.Data.Models;

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public record End {
    public int EndId { get; set; }
    public int RoundId { get; set; }
    public virtual ICollection<ArrowValue> Scores { get; set; } = new List<ArrowValue>();
}