using GroupeScolaire.Application.Common.Interfaces;
using GroupeScolaire.Domain.Entities;

namespace GroupeScolaire.Infrastructure.Persistence.Repositories;

public class TenantsRepository : ITenantsRepository
{
    private readonly TenantsDbContext _context;

    public TenantsRepository(TenantsDbContext context)
    {
        _context = context;
    }

    public Tenant? GetById(Guid id) => _context.Tenants.FirstOrDefault(t => t.Id == id);
}