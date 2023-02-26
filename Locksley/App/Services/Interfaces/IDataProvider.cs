using Locksley.App.Attributes;

namespace Locksley.App.Services.Interfaces; 

[ServiceLifetime(Lifetime = ServiceLifetime.Singleton)]
public interface IDataProvider<T> where T : class {
    T? Get(int id);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetPage(int pageNumber, int pageSize);
    void Save(T item);
    void SaveMany(IEnumerable<T> items);
    void Delete(T item);
    void DeleteMany(IEnumerable<T> items);
}