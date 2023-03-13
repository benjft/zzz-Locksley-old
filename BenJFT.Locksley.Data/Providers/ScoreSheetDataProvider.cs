using BenJFT.Locksley.Data.Models;
using BenJFT.Locksley.Data.Providers.Interfaces;

namespace BenJFT.Locksley.Data.Providers;

public class ScoreSheetDataProvider : IDataProvider<ScoreSheet> {
    private readonly LocksleyDbContext _dbContext;

    public ScoreSheetDataProvider(LocksleyDbContext dbContext) {
        _dbContext = dbContext;
    }

    public ScoreSheet? Get(int id) {
        return _dbContext.ScoreSheets.Find(id);
    }

    public IEnumerable<ScoreSheet> GetAll() {
        return _dbContext.ScoreSheets.OrderBy(ss => ss.ScoreSheetId)
            .AsEnumerable();
    }

    public IEnumerable<ScoreSheet> GetPage(int pageNumber, int pageSize) {
        return _dbContext.ScoreSheets.OrderBy(ss => ss.ScoreSheetId)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .AsEnumerable();
    }

    public void Save() {
        SaveMany(Array.Empty<ScoreSheet>());
    }

    public void Save(ScoreSheet item) {
        SaveMany(new[] {item});
    }

    public void SaveMany(IEnumerable<ScoreSheet> items) {
        _dbContext.ScoreSheets.AddRange(items.Where(i => !_dbContext.Entry(i).IsKeySet));

        _dbContext.SaveChanges();
    }

    public void Delete(ScoreSheet item) {
        DeleteMany(new[] {item});
    }

    public void DeleteMany(IEnumerable<ScoreSheet> items) {
        _dbContext.RemoveRange(items);
        _dbContext.SaveChanges();
    }
}