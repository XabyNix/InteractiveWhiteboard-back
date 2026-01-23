using Microsoft.AspNetCore.Cors.Infrastructure;

namespace InteractiveWhiteboard_back.Extensions;

internal static class ServicesExtensions
{
    public static void ConfigureCors(this IServiceCollection services, IConfigurationManager configuration)
    {
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins")
            .Get<string[]>();
        
        services.AddCors(opt =>
        {
            opt.AddPolicy("allowWhiteBoard",
                policy =>
                {
                    policy.WithOrigins(allowedOrigins ?? [])
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });

    }
}