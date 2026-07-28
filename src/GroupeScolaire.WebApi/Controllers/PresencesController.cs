using GroupeScolaire.Application.Presences.Commands.CreatePresence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroupeScolaire.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PresencesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PresencesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePresenceCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { id });
    }
}