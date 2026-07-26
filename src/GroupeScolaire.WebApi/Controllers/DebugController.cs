using GroupeScolaire.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GroupeScolaire.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DebugController : ControllerBase
{
    private readonly ITenantProvider _tenantProvider;

    public DebugController(ITenantProvider tenantProvider)
    {
        _tenantProvider = tenantProvider;
    }

    [HttpGet("tenant-info")]
    public IActionResult GetTenantInfo()
    {
        return Ok(new
        {
            TenantId = _tenantProvider.TenantId,
            ConnectionString = _tenantProvider.ConnectionString
        });
    }
}