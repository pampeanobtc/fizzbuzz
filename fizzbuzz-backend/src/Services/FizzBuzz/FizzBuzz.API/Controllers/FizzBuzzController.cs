using System.Net;
using FizzBuzz.Application.Features.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzz.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class FizzBuzzController : ControllerBase
{
    private readonly IMediator _mediator;

    public FizzBuzzController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    // testing purpose
    [HttpPost(Name = "Play")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> Play([FromBody] ProcessFizzBuzzCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}