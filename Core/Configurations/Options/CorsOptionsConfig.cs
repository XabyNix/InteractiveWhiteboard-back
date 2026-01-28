namespace Core.Configurations.Options;

public record CorsOptionsConfig
{
    public string[] AllowedOrigins { get; set; } = [];
}