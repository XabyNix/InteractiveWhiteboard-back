using Application.Dto;
using InteractiveWhiteboard_back.Hubs.MethodNames;
using Microsoft.AspNetCore.SignalR;

namespace InteractiveWhiteboard_back.Hubs;

public sealed class StrokeHub : Hub
{
    public async Task SendStroke(StrokeDto input)
    {
        await Clients.Others.SendAsync(ActionNames.ReceiveStroke, input);
    }

    public async Task SendPoint(PointDto input)
    {
        await Clients.Others.SendAsync(ActionNames.ReceivePoint, input);
    }
    
    public async Task Clear()
    {
        await Clients.Others.SendAsync(ActionNames.Clear);
    }
    
    
}