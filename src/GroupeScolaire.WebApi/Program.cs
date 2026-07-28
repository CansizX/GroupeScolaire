using GroupeScolaire.Application.Common.Interfaces;
using GroupeScolaire.Application.Eleves.Commands.CreateEleve;
using GroupeScolaire.Infrastructure.Persistence;
using GroupeScolaire.Infrastructure.Persistence.Repositories;
using GroupeScolaire.Infrastructure.Services;
using GroupeScolaire.Infrastructure.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantProvider, JwtTenantProvider>();
// BD maître (Tenants)
builder.Services.AddDbContext<TenantsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TenantsDb")));

builder.Services.AddScoped<ITenantsRepository, TenantsRepository>();

// BD applicative (par tenant, résolue dynamiquement)
builder.Services.AddScoped<IEtablissementDbContext>(sp =>
{
    var tenantProvider = sp.GetRequiredService<ITenantProvider>();
    var connectionString = tenantProvider.ConnectionString
        ?? throw new InvalidOperationException("Tenant introuvable ou non spécifié.");

    var options = new DbContextOptionsBuilder<EtablissementDbContext>()
        .UseSqlServer(connectionString)
        .Options;

    return new EtablissementDbContext(options);
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateEleveCommand).Assembly));
builder.Services.AddSignalR();
builder.Services.AddScoped<IPresenceNotifier, PresenceNotifier>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowTestClient", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;
var jwtAudience = builder.Configuration["Jwt:Audience"]!;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowTestClient");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<PresenceHub>("/hubs/presence");


app.Run();