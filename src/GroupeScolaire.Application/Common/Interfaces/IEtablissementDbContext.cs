using GroupeScolaire.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GroupeScolaire.Application.Common.Interfaces;

public interface IEtablissementDbContext
{
    DbSet<Eleve> Eleves { get; }
    DbSet<Staff> Staffs { get; }
    DbSet<Presence> Presences { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}