using GroupeScolaire.Application.Eleves.Commands.CreateEleve;
using GroupeScolaire.Application.Eleves.Queries.GetElevesByClasse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroupeScolaire.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ElevesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ElevesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEleveCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { id });
    }
    [HttpGet("classe/{classeId}")]
    public async Task<IActionResult> GetByClasse(Guid classeId)
    {
        var eleves = await _mediator.Send(new GetElevesByClasseQuery(classeId));
        return Ok(eleves);
    }
}