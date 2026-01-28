using System.ComponentModel.DataAnnotations;

namespace Core.Configurations.Options;

public record JwtOptions
{
    public const string SectionName = "Jwt";
    
    [Required]
    public required string Issuer { get; init; }
    
    [Required]
    public required string Audience { get; init; }
    
    [Required]
    public required string SecurityKey { get; init; }
    
    [Range(1, 15)]
    public required ushort ExpireInMinutes { get; init; }
    
    [Required]
    public required string ClientId { get; init; }
    
    [Required]
    public required string ClientSecret { get; init; }
}