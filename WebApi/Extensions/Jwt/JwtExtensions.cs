using Core.Configurations.Options;
using InteractiveWhiteboard_back.Configurations.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace InteractiveWhiteboard_back.Extensions.Jwt;

public static class JwtExtensions
{
    private static void AddJwtOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetRequiredSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
    
    public static void AddJwtAuthentication(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddJwtOptions(configuration);
        
        services.ConfigureOptions<ConfigureJwtBearerOptions>();
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer();
    }
}