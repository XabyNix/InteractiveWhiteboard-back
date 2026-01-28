using System.Security.Claims;
using System.Text;
using Asp.Versioning;
using Core.Configurations.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace InteractiveWhiteboard_back.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController(IOptions<JwtOptions> jwtOptions) : ControllerBase
{
    [HttpPost("token")]
    public async Task<IActionResult> GetTokenAsync(TokenRequest req, CancellationToken ct)
    {
        
        var isClientIdValid = req.ClientId.Equals(jwtOptions.Value.ClientId, StringComparison.Ordinal);
        if (!isClientIdValid)
            return BadRequest();
        
        var isClientSecretValid = req.ClientSecret.Equals(jwtOptions.Value.ClientSecret, StringComparison.Ordinal);
        if (!isClientSecretValid)
            return BadRequest();
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            jwtOptions.Value.SecurityKey));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(),
            Expires = DateTime.UtcNow.AddMinutes(jwtOptions.Value.ExpireInMinutes),
            Audience = jwtOptions.Value.Audience,
            Issuer = jwtOptions.Value.Issuer,
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
        };
        
        var tokenHandler = new JsonWebTokenHandler();
        var expiresIn = (int)(tokenDescriptor.Expires - DateTime.UtcNow).Value.TotalSeconds;
        
        var response = new TokenResponse(tokenHandler.CreateToken(tokenDescriptor), expiresIn);

        return Ok(response);

    }
    
}