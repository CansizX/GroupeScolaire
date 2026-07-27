using GroupeScolaire.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GroupeScolaire.Infrastructure.Persistence;

public class EtablissementDesignTimeDbContextFactory : IDesignTimeDbContextFactory<EtablissementDbContext>
{
    public EtablissementDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EtablissementDbContext>();

        // Connection string "modèle" juste pour générer la migration —
        // sera remplacée dynamiquement à l'exécution par le tenant
        var connectionString = "Server=localhost,1433;Database=Etablissement_Template_Db;User Id=sa;Password=Passe_Code123!;TrustServerCertificate=True";

        optionsBuilder.UseSqlServer(connectionString);

        return new EtablissementDbContext(optionsBuilder.Options);
    }
}