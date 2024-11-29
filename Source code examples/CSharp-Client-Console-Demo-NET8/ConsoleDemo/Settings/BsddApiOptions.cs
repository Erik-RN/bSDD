namespace ConsoleDemo.Settings;

public record BsddApiOptions
{
    public string BaseUrl { get; init; }
    public string ClientId { get; init; }
    public string Scope { get; init; }
    public string TenantId { get; init; }
}