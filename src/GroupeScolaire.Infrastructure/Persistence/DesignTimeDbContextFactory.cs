using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GroupeScolaire.Infrastructure.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TenantsDbContext>
{
    public TenantsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TenantsDbContext>();

        var connectionString = Environment.GetEnvironmentVariable("TENANTS_DB_CONNECTION")
                               ?? "Server=localhost,1433;Database=TenantsDb;User Id=sa;Password=Passe_Code123!;TrustServerCertificate=True";

        optionsBuilder.UseSqlServer(connectionString);

        return new TenantsDbContext(optionsBuilder.Options);
    }
}