using GroupeScolaire.Application.Common.Interfaces;
using GroupeScolaire.Infrastructure.Persistence;
using GroupeScolaire.Infrastructure.Persistence.Repositories;
using GroupeScolaire.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantProvider, TenantProvider>();

// BD maître (Tenants)
builder.Services.AddDbContext<TenantsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TenantsDb")));

builder.Services.AddScoped<ITenantsRepository, TenantsRepository>();

// BD applicative (par tenant, résolue dynamiquement)
builder.Services.AddScoped<EtablissementDbContext>(sp =>
{
    var tenantProvider = sp.GetRequiredService<ITenantProvider>();
    var connectionString = tenantProvider.ConnectionString
        ?? throw new InvalidOperationException("Tenant introuvable ou non spécifié.");

    var options = new DbContextOptionsBuilder<EtablissementDbContext>()
        .UseSqlServer(connectionString)
        .Options;

    return new EtablissementDbContext(options);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();