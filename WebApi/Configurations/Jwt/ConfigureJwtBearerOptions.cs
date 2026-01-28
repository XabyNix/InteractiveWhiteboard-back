using System.Text;
using Core.Configurations.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace InteractiveWhiteboard_back.Configurations.Jwt;

public class ConfigureJwtBearerOptions(IOptions<JwtOptions> jwtConfig) : IConfigureNamedOptions<JwtBearerOptions>
{
    public void Configure(string? name, JwtBearerOptions options)
    {
        if (name != JwtBearerDefaults.AuthenticationScheme) return;
        Configure(options);
    }
    
    public void Configure(JwtBearerOptions options)
    {
        options.TokenHandlers.Clear();
        options.TokenHandlers.Add(new JsonWebTokenHandler());
        
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtConfig.Value.Issuer,

            ValidateAudience = true,
            ValidAudiences = [jwtConfig.Value.Audience],

            ValidateIssuerSigningKey = true,
            ValidAlgorithms = [SecurityAlgorithms.HmacSha256],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtConfig.Value.SecurityKey)),

            ValidateLifetime = true
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                
                var path = context.HttpContext.Request.Path;

                if (!string.IsNullOrWhiteSpace(accessToken) &&
                    path.StartsWithSegments("/hub"))
                {
                    context.Token = accessToken;
                }
                    
                return Task.CompletedTask;
            },
            
        };

    }

    
}