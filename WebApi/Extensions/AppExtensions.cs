using InteractiveWhiteboard_back.Hubs;

namespace InteractiveWhiteboard_back.Extensions;

public static class AppExtensions
{
    public static void MapSignalrHubs(this IEndpointRouteBuilder app)
    {
        app.MapHub<StrokeHub>("/hub/stroke");

    }
    
}