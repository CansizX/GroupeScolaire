using MediatR;

namespace GroupeScolaire.Application.Presences.Queries.GetPresencesByPersonne;

public record GetPresencesByPersonneQuery(Guid PersonneId) : IRequest<List<PresenceDto>>;

public record PresenceDto(Guid Id, Guid PersonneId, string TypePersonne, DateTime DateHeure, string Statut);