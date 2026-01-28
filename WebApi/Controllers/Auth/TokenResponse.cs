namespace InteractiveWhiteboard_back.Controllers.Auth;

public record TokenResponse(string AccessToken, int ExpiresIn);