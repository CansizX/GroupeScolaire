using Microsoft.AspNetCore.SignalR;

namespace GroupeScolaire.Infrastructure.Hubs;

public class PresenceHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var tenantId = Context.GetHttpContext()?.Request.Query["tenantId"].FirstOrDefault()?.ToLowerInvariant();
        if (!string.IsNullOrEmpty(tenantId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, tenantId);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var tenantId = Context.GetHttpContext()?.Request.Query["tenantId"].FirstOrDefault();

        if (!string.IsNullOrEmpty(tenantId))
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, tenantId);
        }

        await base.OnDisconnectedAsync(exception);
    }
}