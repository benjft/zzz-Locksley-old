namespace Locksley.App.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class ServiceAttribute : Attribute {
    public required ServiceLifetime Lifetime { get; init; }
}