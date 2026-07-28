using GroupeScolaire.Application.Staffs.Commands.CreateStaff;
using GroupeScolaire.Application.Staffs.Queries.GetStaffsByRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupeScolaire.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StaffsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StaffsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Direction")]
    public async Task<IActionResult> Create(CreateStaffCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { id });
    }

    [HttpGet("role/{role}")]
    public async Task<IActionResult> GetByRole(string role)
    {
        var staffs = await _mediator.Send(new GetStaffsByRoleQuery(role));
        return Ok(staffs);
    }
}