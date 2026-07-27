using GroupeScolaire.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroupeScolaire.Application.Eleves.Queries.GetElevesByClasse;

public class GetElevesByClasseHandler : IRequestHandler<GetElevesByClasseQuery, List<EleveDto>>
{
    private readonly IEtablissementDbContext _context;

    public GetElevesByClasseHandler(IEtablissementDbContext context)
    {
        _context = context;
    }

    public async Task<List<EleveDto>> Handle(GetElevesByClasseQuery request, CancellationToken cancellationToken)
    {
        return await _context.Eleves
            .Where(e => e.ClasseId == request.ClasseId)
            .Select(e => new EleveDto(e.Id, e.Nom, e.Prenom, e.DateNaissance))
            .ToListAsync(cancellationToken);
    }
}