namespace InteractiveWhiteboard_back.Extensions;

internal static class ServicesExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy("allowWhiteBoard",
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200", "http://localhost:4200/")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });

    }
}