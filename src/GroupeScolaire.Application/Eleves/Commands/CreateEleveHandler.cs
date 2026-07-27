using GroupeScolaire.Application.Common.Interfaces;
using GroupeScolaire.Domain.Entities;
using MediatR;

namespace GroupeScolaire.Application.Eleves.Commands.CreateEleve;

public class CreateEleveHandler : IRequestHandler<CreateEleveCommand, Guid>
{
    private readonly IEtablissementDbContext _context;

    public CreateEleveHandler(IEtablissementDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateEleveCommand request, CancellationToken cancellationToken)
    {
        var eleve = new Eleve
        {
            Id = Guid.NewGuid(),
            Nom = request.Nom,
            Prenom = request.Prenom,
            DateNaissance = request.DateNaissance,
            ClasseId = request.ClasseId
        };

        _context.Eleves.Add(eleve);
        await _context.SaveChangesAsync(cancellationToken);

        return eleve.Id;
    }
}