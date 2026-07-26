using GroupeScolaire.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GroupeScolaire.Infrastructure.Services;

public class TenantProvider : ITenantProvider
{
    private const string TenantHeaderName = "X-Tenant-Id";

    public Guid? TenantId { get; }
    public string? ConnectionString { get; }

    public TenantProvider(IHttpContextAccessor httpContextAccessor, ITenantsRepository tenantsRepository)
    {
        var header = httpContextAccessor.HttpContext?.Request.Headers[TenantHeaderName].FirstOrDefault();

        if (Guid.TryParse(header, out var tenantId))
        {
            TenantId = tenantId;
            var tenant = tenantsRepository.GetById(tenantId);
            ConnectionString = tenant?.ConnectionString;
        }
    }
}