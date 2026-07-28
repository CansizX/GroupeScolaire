using GroupeScolaire.Application.Common.Interfaces;
using GroupeScolaire.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GroupeScolaire.Infrastructure.Services;

public class PresenceNotifier : IPresenceNotifier
{
    private readonly IHubContext<PresenceHub> _hubContext;

    public PresenceNotifier(IHubContext<PresenceHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyPresenceCreated(string tenantId, Guid presenceId, Guid personneId, string statut)
    {
        Console.WriteLine($"[DEBUG] Envoi notification au groupe: '{tenantId}'");
        await _hubContext.Clients.Group(tenantId).SendAsync("PresenceCreated", new
        {
            presenceId,
            personneId,
            statut
        });
    }
}