using Locksley.App.Data;
using Locksley.App.Data.Models;
using Locksley.App.Services.Interfaces;

namespace Locksley.App.Services; 

public class ScoreSheetRepository : IRepository<ScoreSheet> {
    private readonly LocksleyDbContext _dbContext;

    public ScoreSheetRepository(LocksleyDbContext dbContext) {
        _dbContext = dbContext;
    }

    public ScoreSheet? Get(int id) => _dbContext.ScoreSheets.Find(id);

    public IEnumerable<ScoreSheet> GetAll() => 
        _dbContext.ScoreSheets
            .OrderBy(ss => ss.ScoreSheetId)
            .AsEnumerable();

    public IEnumerable<ScoreSheet> GetPage(int pageNumber, int pageSize) =>
        _dbContext.ScoreSheets
            .OrderBy(ss => ss.ScoreSheetId)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .AsEnumerable();

    public void Save(ScoreSheet item) => SaveMany(new[] {item});

    public void SaveMany(IEnumerable<ScoreSheet> items) {
        _dbContext.ScoreSheets.AddRange(items.Where(i => !_dbContext.Entry(i).IsKeySet));

        _dbContext.SaveChanges();
    }

    public void Delete(ScoreSheet item) => DeleteMany(new[] {item});

    public void DeleteMany(IEnumerable<ScoreSheet> items) {
        _dbContext.RemoveRange(items);
        _dbContext.SaveChanges();
    }
}