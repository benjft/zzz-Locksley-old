using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Locksley.App.Models;

public record End {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [TextBlob(nameof(ScoresBlob))]
    public List<int> Scores { get; set; } = new();
    
    public string ScoresBlob { get; set; } = default!;
}