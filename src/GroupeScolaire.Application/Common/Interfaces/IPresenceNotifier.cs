namespace GroupeScolaire.Application.Common.Interfaces;

public interface IPresenceNotifier
{
    Task NotifyPresenceCreated(string tenantId, Guid presenceId, Guid personneId, string statut);
}