namespace BenJFT.Locksley.Common.Helpers;

public static class EnumerableExtensions {
    public static (T? head, IEnumerable<T> tail) Pop<T>(this IEnumerable<T> enumerable) {
        var list = enumerable.ToList();
        return (list.FirstOrDefault(), list.Skip(1));
    }
}