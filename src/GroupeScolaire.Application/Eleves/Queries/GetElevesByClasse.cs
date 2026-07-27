using MediatR;

namespace GroupeScolaire.Application.Eleves.Queries.GetElevesByClasse;

public record GetElevesByClasseQuery(Guid ClasseId) : IRequest<List<EleveDto>>;

public record EleveDto(Guid Id, string Nom, string Prenom, DateOnly DateNaissance);