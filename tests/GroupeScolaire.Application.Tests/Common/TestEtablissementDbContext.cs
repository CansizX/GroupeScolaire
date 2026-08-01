using GroupeScolaire.Application.Common.Interfaces;
using GroupeScolaire.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GroupeScolaire.Application.Tests.Common;

public class TestEtablissementDbContext : DbContext, IEtablissementDbContext
{
    public TestEtablissementDbContext(DbContextOptions<TestEtablissementDbContext> options) : base(options) { }

    public DbSet<Eleve> Eleves => Set<Eleve>();
    public DbSet<Staff> Staffs => Set<Staff>();
    public DbSet<Presence> Presences => Set<Presence>();
}

public static class TestDbContextFactory
{
    public static TestEtablissementDbContext Create()
    {
        var options = new DbContextOptionsBuilder<TestEtablissementDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new TestEtablissementDbContext(options);
    }
}