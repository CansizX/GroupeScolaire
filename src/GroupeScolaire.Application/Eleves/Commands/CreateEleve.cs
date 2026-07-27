using MediatR;

namespace GroupeScolaire.Application.Eleves.Commands.CreateEleve;

public record CreateEleveCommand(
    string Nom,
    string Prenom,
    DateOnly DateNaissance,
    Guid ClasseId
) : IRequest<Guid>;