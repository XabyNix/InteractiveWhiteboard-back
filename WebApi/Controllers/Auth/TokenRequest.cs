namespace InteractiveWhiteboard_back.Controllers.Auth;

public record TokenRequest
{
    public required string  ClientId { get; set; }
    public required string  ClientSecret { get; set; }
}