namespace Core.Configurations.Models;

public record CorsOptionsConfig
{
    public string[] AllowedOrigins { get; set; } = [];
}