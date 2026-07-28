using GroupeScolaire.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GroupeScolaire.Infrastructure.Services;

public class JwtTenantProvider : ITenantProvider
{
    public Guid? TenantId { get; }
    public string? ConnectionString { get; }

    public JwtTenantProvider(IHttpContextAccessor httpContextAccessor, ITenantsRepository tenantsRepository)
    {
        var tenantClaim = httpContextAccessor.HttpContext?.User.FindFirst("tenantId")?.Value;

        if (Guid.TryParse(tenantClaim, out var tenantId))
        {
            TenantId = tenantId;
            var tenant = tenantsRepository.GetById(tenantId);
            ConnectionString = tenant?.ConnectionString;
        }
    }
}