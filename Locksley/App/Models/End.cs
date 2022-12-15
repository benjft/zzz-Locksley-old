namespace Locksley.App.Models;

public record End {
    public int EndId { get; set; }

    public virtual ICollection<ArrowValue> Scores { get; set; } = new List<ArrowValue>();
}