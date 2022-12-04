using SQLite;

namespace Locksley.App; 

public static class Constants {
    public const string DatabaseFilename = "Locksley.sqlite3";

    public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    public const SQLiteOpenFlags DatabaseOpenFlags =
        SQLiteOpenFlags.ReadWrite |
        SQLiteOpenFlags.Create |
        SQLiteOpenFlags.SharedCache;
}