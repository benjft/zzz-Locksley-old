namespace BenJFT.Locksley.Data.Models;

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public record ScoreSheet {
    public int ScoreSheetId { get; set; }
    public string Title { get; set; } = "";
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string DateString => CreatedDate.ToShortDateString();
    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
}