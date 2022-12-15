namespace Locksley.App.Models;

public record ScoreSheet {
    public int ScoreSheetId { get; set; }

    public string Title { get; set; } = "";

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();
}