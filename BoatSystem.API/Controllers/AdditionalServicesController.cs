using BoatSystem.Application.Commands.AdditionalServiceCommand;
using BoatSystem.Application.Queries.AdditionalServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AdditionalServicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdditionalServicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/AdditionalServices
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAdditionalServiceCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    // PUT: api/AdditionalServices/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAdditionalServiceCommand command)
    {
        if (id != command.UpdateAdditionalServiceDto.Id)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/AdditionalServices/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) // تعديل هنا
    {
        var command = new DeleteAdditionalServiceCommand { Id = id };
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // GET: api/AdditionalServices/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) // تعديل هنا
    {
        var query = new GetAdditionalServiceByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // GET: api/AdditionalServices
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllAdditionalServicesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }


}
