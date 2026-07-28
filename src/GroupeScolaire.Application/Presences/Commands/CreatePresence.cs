using GroupeScolaire.Application.Common.Interfaces;
using GroupeScolaire.Domain.Entities;
using MediatR;

namespace GroupeScolaire.Application.Presences.Commands.CreatePresence;

public record CreatePresenceCommand(
    Guid PersonneId,
    string TypePersonne,
    string Statut
) : IRequest<Guid>;
public class CreatePresenceHandler : IRequestHandler<CreatePresenceCommand, Guid>
{
    private readonly IEtablissementDbContext _context;
    private readonly ITenantProvider _tenantProvider;
    private readonly IPresenceNotifier _notifier;

    public CreatePresenceHandler(
        IEtablissementDbContext context,
        ITenantProvider tenantProvider,
        IPresenceNotifier notifier)
    {
        _context = context;
        _tenantProvider = tenantProvider;
        _notifier = notifier;
    }

    public async Task<Guid> Handle(CreatePresenceCommand request, CancellationToken cancellationToken)
    {
        var presence = new Presence
        {
            Id = Guid.NewGuid(),
            PersonneId = request.PersonneId,
            TypePersonne = request.TypePersonne,
            DateHeure = DateTime.UtcNow,
            Statut = request.Statut
        };

        _context.Presences.Add(presence);
        await _context.SaveChangesAsync(cancellationToken);

        var tenantId = _tenantProvider.TenantId?.ToString().ToLowerInvariant() ?? string.Empty;
        await _notifier.NotifyPresenceCreated(tenantId, presence.Id, presence.PersonneId, presence.Statut);

        return presence.Id;
    }
}