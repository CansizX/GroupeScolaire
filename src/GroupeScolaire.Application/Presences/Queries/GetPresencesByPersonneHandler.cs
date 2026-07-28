using GroupeScolaire.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroupeScolaire.Application.Presences.Queries.GetPresencesByPersonne;

public class GetPresencesByPersonneHandler : IRequestHandler<GetPresencesByPersonneQuery, List<PresenceDto>>
{
    private readonly IEtablissementDbContext _context;

    public GetPresencesByPersonneHandler(IEtablissementDbContext context)
    {
        _context = context;
    }

    public async Task<List<PresenceDto>> Handle(GetPresencesByPersonneQuery request, CancellationToken cancellationToken)
    {
        return await _context.Presences
            .Where(p => p.PersonneId == request.PersonneId)
            .OrderByDescending(p => p.DateHeure)
            .Select(p => new PresenceDto(p.Id, p.PersonneId, p.TypePersonne, p.DateHeure, p.Statut))
            .ToListAsync(cancellationToken);
    }
}