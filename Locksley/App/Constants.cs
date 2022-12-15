namespace Locksley.App;

public static class Constants {
    public const string DatabaseFilename = "Locksley.sqlite3";

    public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}