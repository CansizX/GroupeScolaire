using GroupeScolaire.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GroupeScolaire.Infrastructure.Persistence;

public class TenantsDbContext : DbContext
{
    public TenantsDbContext(DbContextOptions<TenantsDbContext> options) : base(options) { }

    public DbSet<Tenant> Tenants => Set<Tenant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Nom).IsRequired().HasMaxLength(200);
            entity.Property(t => t.ConnectionString).IsRequired();
        });
    }
}