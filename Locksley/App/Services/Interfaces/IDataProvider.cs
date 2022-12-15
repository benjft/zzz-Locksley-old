using Locksley.App.Models;

namespace Locksley.App.Services.Interfaces; 

public interface IDataProvider {
    IEnumerable<ScoreSheet> GetAllScoreSheets();
}