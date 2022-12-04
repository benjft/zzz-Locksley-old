using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Locksley.App.Models; 

[Table(nameof(ScoreSheet))]
public record ScoreSheet {

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    public string Title { get; set; } = "";

    [Indexed]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [OneToMany]
    public List<Round> Rounds { get; set; } = new();
}