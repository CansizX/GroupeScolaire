using GroupeScolaire.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroupeScolaire.Application.Staffs.Queries.GetStaffsByRole;

public class GetStaffsByRoleHandler : IRequestHandler<GetStaffsByRoleQuery, List<StaffDto>>
{
    private readonly IEtablissementDbContext _context;

    public GetStaffsByRoleHandler(IEtablissementDbContext context)
    {
        _context = context;
    }

    public async Task<List<StaffDto>> Handle(GetStaffsByRoleQuery request, CancellationToken cancellationToken)
    {
        return await _context.Staffs
            .Where(s => s.Role == request.Role)
            .Select(s => new StaffDto(s.Id, s.Nom, s.Prenom, s.Role))
            .ToListAsync(cancellationToken);
    }
}