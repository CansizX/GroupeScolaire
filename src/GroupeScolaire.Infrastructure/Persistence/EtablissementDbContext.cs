using GroupeScolaire.Application.Common.Interfaces;
using GroupeScolaire.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GroupeScolaire.Infrastructure.Persistence;

public class EtablissementDbContext : DbContext , IEtablissementDbContext
{
    public EtablissementDbContext(DbContextOptions<EtablissementDbContext> options) : base(options) { }

    public DbSet<Eleve> Eleves => Set<Eleve>();
    public DbSet<Staff> Staffs => Set<Staff>();
    public DbSet<Presence> Presences => Set<Presence>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Eleve>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Nom).IsRequired().HasMaxLength(100);
            e.Property(x => x.Prenom).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Staff>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Nom).IsRequired().HasMaxLength(100);
            e.Property(x => x.Role).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Presence>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.TypePersonne).IsRequired().HasMaxLength(20);
            e.Property(x => x.Statut).IsRequired().HasMaxLength(20);
        });
    }
}