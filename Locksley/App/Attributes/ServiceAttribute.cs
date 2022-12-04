namespace Locksley.App.Attributes; 

public class ServiceAttribute : Attribute {
    public required ServiceLifetime Lifetime { get; init; }
}